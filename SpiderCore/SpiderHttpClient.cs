using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using SpiderCore.RequestStrategies;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace SpiderCore
{
    public class SpiderHttpClient : ISpiderHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<SpiderHttpClient> _logger;

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
            setHttpClientBaseAddress(serviceName);


        }

        private void setHttpClientBaseAddress(string serviceName)
        {
            var service = Options.Services.Where(o => o.ServiceName.Equals(serviceName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (service == null)
            {
                throw new Exception($"{service.ServiceName} not exsit");
            }
            var requestStrategy = RequestStrategyFactory.Instance.CreateRequestStrategy(service);
            string ip = requestStrategy?.GetServiceIp();
            if (string.IsNullOrEmpty(ip))
            {
                throw new Exception($"{service.ServiceName} not found service ip");
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
                setHttpClientBaseAddress(ServiceName); 
            }

            return response;
        }
    }

}
