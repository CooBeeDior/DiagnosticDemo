using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using SpiderCore.RequestStrategies;
using Microsoft.Extensions.Logging;
using System.Threading;
using Polly;
using System.Net;
using Polly.Timeout;
using DiagnosticModel;

namespace SpiderCore
{
    public class SpiderHttpClient : ISpiderHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<SpiderHttpClient> _logger;

        private readonly string ServiceName;
        protected readonly SpiderOptions Options;

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
            var response = await trySendAsync(request);
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


        private async Task<HttpResponseMessage> trySendAsync(HttpRequestMessage request)
        { 
            #region 超时
            var policyTimeout = Policy.TimeoutAsync(Options.SpiderHttpRequestOptions.Timeout, TimeoutStrategy.Pessimistic,
              async (context, timespan, task) =>
              {
                  _logger.LogWarning($"请求url:{request.RequestUri}超时{timespan.TotalMilliseconds}ms,\r\n body: {request.Content.ToStr()} ");
              }).AsAsyncPolicy<HttpResponseMessage>();
            #endregion

            #region 错误异常 重试
            var policyException = Policy.Handle<HttpRequestException>()
                .RetryAsync(Options.SpiderHttpRequestOptions.RetryCount, async (exception, retryCount, context) =>
                {
                    _logger.LogWarning(exception, $"请求url:{request.RequestUri}出错,重试次数:{retryCount}次,\r\n body: {request.Content.ToStr()} ");
                    //重试 重新去获取新的baseurl
                    setHttpClientBaseAddress(ServiceName);

                }).AsAsyncPolicy<HttpResponseMessage>();
            #endregion

            #region 超过最大请求并发数
            var policyBulkhead = Policy.BulkheadAsync(Options.SpiderHttpRequestOptions.MaxParallelization, async (context) =>
            {
                _logger.LogWarning($"请求url:{request.RequestUri}超过请求{Options.SpiderHttpRequestOptions.MaxParallelization}并发数,\r\n body: {request.Content.ToStr()} ");

                // do something
            }).AsAsyncPolicy<HttpResponseMessage>();
            #endregion

            #region 熔断  
            //80%出错就熔断， 第二个参数  多长时间内出现80%错误，
            //第三个参数  当请求比较少时候，最少的请求数，
            //下面就是  10秒内 请求小于100个就还不需要熔断。第四个参数 熔断时间长度

            var policyAdvancedCircuitBreaker = Policy.Handle<HttpRequestException>().AdvancedCircuitBreakerAsync(
                   failureThreshold: Options.SpiderHttpRequestOptions.FailureThreshold,
                   samplingDuration: Options.SpiderHttpRequestOptions.SamplingDuration,
                   minimumThroughput: Options.SpiderHttpRequestOptions.MinimumThroughput,
                   durationOfBreak: Options.SpiderHttpRequestOptions.DurationOfBreak,
                   onBreak: (r, t) => { },
                   onReset: () => { },
                   onHalfOpen: () => { }).AsAsyncPolicy<HttpResponseMessage>();
            #endregion

            #region 降级
            var policyFallback = Policy.Handle<HttpRequestException>()
                .FallbackAsync(async (exception, context, cancellationToken) =>
                {

                }, async (exception, context) =>
                {

                }).AsAsyncPolicy<HttpResponseMessage>();
            #endregion


            return await Policy.WrapAsync<HttpResponseMessage>(policyTimeout, policyException, policyBulkhead, policyAdvancedCircuitBreaker).ExecuteAsync(async () =>
             {
                 var response = await _httpClient.SendAsync(request);
                 return response;
             });

        }
    }

}
