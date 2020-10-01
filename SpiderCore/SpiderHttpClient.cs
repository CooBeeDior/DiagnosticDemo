using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using SpiderCore.RequestStrategies;

namespace SpiderCore
{
    public class SpiderHttpClient : ISpiderHttpClient
    {
        private readonly HttpClient HttpClient;
        private readonly IHttpContextAccessor HttpContextAccessor;
        //后期根据serviceName去拿baseUrl
        private readonly string ServiceName;
        protected readonly SpiderOptions Options;
        public SpiderHttpClient(string serviceName, IServiceProvider serviceProvider)
        {
            Options = serviceProvider.GetService<SpiderOptions>();
            HttpClient = serviceProvider.GetService<IHttpClientFactory>().CreateClient(nameof(SpiderHttpClient));
            ServiceName = serviceName;
            var service = Options.Services.Where(o => o.ServiceName.Equals(serviceName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            var ip = RequestStrategyFactory.Instance.CreateRequestStrategy(service)?.GetServiceIp(null);
            if (string.IsNullOrEmpty(ip))
            {
                throw new Exception($"{serviceName} not found service ip");
            }


            HttpClient.BaseAddress = new Uri(ip);
            HttpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();



        }


        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            BeforeSend(request);
            var response = await HttpClient.SendAsync(request);
            AfterSend(request, response);
            return response;
        }


        protected void BeforeSend(HttpRequestMessage request)
        {
            request.Headers.Add("trace.microservice", ServiceName);
        }

        protected void AfterSend(HttpRequestMessage request, HttpResponseMessage response)
        {

        }



    }

}
