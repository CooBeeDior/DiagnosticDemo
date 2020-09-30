using DiagnosticCore.Constant;
using DiagnosticCore.LogCore;
using DiagnosticModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DiagnosticAdapter;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace DiagnosticCore
{
    public interface ISpiderHttpClient
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage);
    }
    public class SpiderHttpClient : ISpiderHttpClient
    {
        private readonly HttpClient _httpClient;
        protected HttpContext HttpContext;
        protected IDiagnosticTraceLogger<SpiderHttpClient> Logger { get; }
        public SpiderHttpClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor,
            IDiagnosticTraceLogger<SpiderHttpClient> logger)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(SpiderHttpClient));
            HttpContext = httpContextAccessor.HttpContext;
            Logger = logger;
        }


        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            BeforeSend(request);
            var response = await _httpClient.SendAsync(request);
            AfterSend(request, response);
            return response;
        }


        protected void BeforeSend(HttpRequestMessage request)
        {
            var tranceInfoBuilder = HttpContext.Items[DiagnosticConstant.GetItemKey(typeof(TraceInfoBuilder).FullName)] as TraceInfoBuilder;
            if (tranceInfoBuilder != null)
            {
                var tranceInfo = tranceInfoBuilder.Build();
                request.Headers.Add(HttpConstant.TRACK_ID, tranceInfo.TrackId);
            }
            request.Headers.Add(DiagnosticConstant.GetItemKey("spider"), "1");
        }

        protected void AfterSend(HttpRequestMessage request, HttpResponseMessage response)
        {
            var parentTraceInfoBuilder = HttpContext.Items[DiagnosticConstant.GetItemKey(typeof(TraceInfoBuilder).FullName)] as TraceInfoBuilder;
            if (parentTraceInfoBuilder != null)
            {
                var parentTraceInfo = parentTraceInfoBuilder.Build();
                var tranceInfoBuilder = TraceInfoBuilder.CreateBuilder().BuildFromTraceInfo(parentTraceInfo).ParentId(parentTraceInfo.Id).HttpRequestMessage(response.RequestMessage)
                    .HttpResponseMessage(response).ElapsedTime(HttpContext.ElapsedTime());
                Logger.LogTrace(tranceInfoBuilder);

            }
        }



    }

    public static class SpiderHttpClientExtensions
    {
        public static Task<HttpResponseMessage> GetAsync(this ISpiderHttpClient client, string url, Action<HttpRequestHeaders> header = null)
        {
            return client.SendAsync(url, HttpMethod.Get, null, header);
        }

        public static Task<HttpResponseMessage> PostAsync(this ISpiderHttpClient client, string url, HttpContent content, Action<HttpRequestHeaders> header = null)
        {
            return client.SendAsync(url, HttpMethod.Post, content, header);
        }
        public static Task<HttpResponseMessage> PutAsync(this ISpiderHttpClient client, string url, HttpContent content, Action<HttpRequestHeaders> header = null)
        {
            return client.SendAsync(url, HttpMethod.Put, content, header);
        }

        public static Task<HttpResponseMessage> PatchAsync(this ISpiderHttpClient client, string url, HttpContent content, Action<HttpRequestHeaders> header = null)
        {
            return client.SendAsync(url, HttpMethod.Patch, content, header);
        }

        public static Task<HttpResponseMessage> HeadAsync(this ISpiderHttpClient client, string url, HttpContent content, Action<HttpRequestHeaders> header = null)
        {
            return client.SendAsync(url, HttpMethod.Head, content, header);
        }


        public static Task<HttpResponseMessage> DeleteAsync(this ISpiderHttpClient client, string url, HttpContent content, Action<HttpRequestHeaders> header = null)
        {
            return client.SendAsync(url, HttpMethod.Delete, content, header);
        }
        public static Task<HttpResponseMessage> TraceAsync(this ISpiderHttpClient client, string url, HttpContent content, Action<HttpRequestHeaders> header = null)
        {
            return client.SendAsync(url, HttpMethod.Trace, content, header);
        }
        public static Task<HttpResponseMessage> SendAsync(this ISpiderHttpClient client, string url, HttpMethod method, HttpContent content, Action<HttpRequestHeaders> header = null)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, url);
            request.Content = content;
            header?.Invoke(request.Headers);
            return client.SendAsync(request);
        }
    }

    /// <summary>
    /// 全局HttpClient监听
    /// </summary>
    public class HttpClientTracingDiagnosticProcessor : IHttpClientTracingDiagnosticProcessor
    {
        public const string LISTENERNAME = "HttpHandlerDiagnosticListener";
        public const string HttpRequestStartName = "System.Net.Http.HttpRequestOut.Start";
        public const string HttpRequestName = "System.Net.Http.Request";
        public const string HttpRequestStopName = "System.Net.Http.HttpRequestOut.Stop";
        public const string HttpResponseName = "System.Net.Http.Response";
        public const string HttpExceptionName = "System.Net.Http.Exception";

        public string ListenerName { get { return LISTENERNAME; } }


        protected IServiceProvider ServiceProvider { get; }

        protected HttpContext HttpContext { get; }

        protected IDiagnosticTraceLogger<HttpClientTracingDiagnosticProcessor> Logger { get; }
        public HttpClientTracingDiagnosticProcessor(IHttpContextAccessor httpContextAccessor, IDiagnosticTraceLogger<HttpClientTracingDiagnosticProcessor> logger)
        {
            Logger = logger;
            //拿不到这个httpcontext,不是scope ，该如何获取
            HttpContext = httpContextAccessor.HttpContext;
        }

        [DiagnosticName(HttpRequestStartName)]
        public void HttpRequestStart(HttpRequestMessage request)
        {
            HttpRequestStartHandle(request);
        }

        [DiagnosticName(HttpRequestName)]
        public void HttpRequest(HttpRequestMessage request)
        {
            HttpRequestHandle(request);
        }

        [DiagnosticName(HttpRequestStopName)]
        public void HttpRequestStop(HttpRequestMessage request)
        {
            HttpRequestStopHandle(request);
        }

        [DiagnosticName(HttpResponseName)]
        public void HttpResponse(HttpResponseMessage response)
        {
            HttpResponseHandle(response);
        }

        [DiagnosticName(HttpExceptionName)]
        public void HttpException(HttpRequestMessage request, Exception exception)
        {
            HttpExceptionHandle(request, exception);
        }




        #region protected


        protected virtual void HttpRequestStartHandle(HttpRequestMessage request)
        {




        }
        protected virtual void HttpRequestHandle(HttpRequestMessage request)
        {
            //var tranceInfoBuilder = HttpContext.Items[DiagnosticConstant.GetItemKey(typeof(TraceInfoBuilder).FullName)] as TraceInfoBuilder;
            //if (tranceInfoBuilder != null)
            //{
            //    var tranceInfo = tranceInfoBuilder.Build();
            //    request.Headers.Add(HttpConstant.TRACK_ID, tranceInfo.TrackId);
            //}


        }
        protected virtual void HttpRequestStopHandle(HttpRequestMessage request)
        {

        }

        protected virtual void HttpResponseHandle(HttpResponseMessage response)
        {
            //var parentTraceInfoBuilder = HttpContext.Items[DiagnosticConstant.GetItemKey(typeof(TraceInfoBuilder).FullName)] as TraceInfoBuilder;
            //if (parentTraceInfoBuilder != null)
            //{
            //    var parentTraceInfo = parentTraceInfoBuilder.Build();
            //    var tranceInfoBuilder = TraceInfoBuilder.CreateBuilder().BuildFromTraceInfo(parentTraceInfo).ParentId(parentTraceInfo.Id).HttpRequestMessage(response.RequestMessage)
            //        .HttpResponseMessage(response).ElapsedTime(getElapsedTime(HttpContext));
            //    Logger.LogTrace(tranceInfoBuilder);

            //}





        }


        protected virtual void HttpExceptionHandle(HttpRequestMessage request, Exception exception)
        {
        }
        #endregion


    }

}
