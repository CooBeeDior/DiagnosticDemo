using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using SpiderCore.RequestStrategies;
using Microsoft.Extensions.Logging;

namespace SpiderCore
{
    public class SpiderHttpClient : ISpiderHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<SpiderHttpClient> _logger;
        //后期根据serviceName去拿baseUrl
        private readonly string ServiceName;
        protected readonly SpiderOptions Options;
        private int _retries = 3;
        public SpiderHttpClient(string serviceName, IServiceProvider serviceProvider)
        {
            Options = serviceProvider.GetService<SpiderOptions>();
            _httpClient = serviceProvider.GetService<IHttpClientFactory>().CreateClient();
            _httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            _logger = serviceProvider.GetService<ILogger<SpiderHttpClient>>();
            ServiceName = serviceName;
            var service = Options.Services.Where(o => o.ServiceName.Equals(serviceName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            setHttpClientBaseAddress(service);
    

        }

        private void setHttpClientBaseAddress(SpiderService spiderService)
        {
            if (spiderService == null)
            {
                throw new Exception($"{spiderService.ServiceName} not exsit");
            }
            var requestStrategy = RequestStrategyFactory.Instance.CreateRequestStrategy(spiderService);
            string ip = requestStrategy?.GetServiceIp();
            if (string.IsNullOrEmpty(ip))
            {
                throw new Exception($"{spiderService.ServiceName} not found service ip");
            }
            _httpClient.BaseAddress = new Uri(ip);
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            await BeforeSendAsync(request);
            var response = await _httpClient.SendAsync(request);
            await AfterSendAsync(request, response);
            return response;
        }


        protected Task BeforeSendAsync(HttpRequestMessage request)
        {
            request.Headers.Add("trace.microservice", ServiceName);
            return Task.CompletedTask;
        }

        protected Task AfterSendAsync(HttpRequestMessage request, HttpResponseMessage response)
        {
            return Task.CompletedTask;
        }


        private async Task<HttpResponseMessage> trySendAsync(HttpRequestMessage request, int currentRetryIndex = 0)
        {
            HttpResponseMessage response = null;
            currentRetryIndex++;
            try
            { 
                response = await _httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                if (currentRetryIndex > _retries)
                {
                    _logger.LogError(ex, "重试超过次数");
                }
            }
   
            return response;
        }
    }

}
