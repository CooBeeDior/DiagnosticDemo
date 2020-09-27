using DiagnosticCore.Constant;
using DiagnosticCore.LogCore;
using DiagnosticModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DiagnosticAdapter;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net.Http;
namespace DiagnosticCore
{
    public class HttpClientTracingDiagnosticProcessor : IHttpClientTracingDiagnosticProcessor
    {
        public const string ListenerName = "HttpHandlerDiagnosticListener";
        public const string HttpRequestStartName = "System.Net.Http.HttpRequestOut.Start";
        public const string HttpRequestName = "System.Net.Http.Request";
        public const string HttpRequestStopName = "System.Net.Http.HttpRequestOut.Stop";
        public const string HttpResponseName = "System.Net.Http.Response";
        public const string HttpExceptionName = "System.Net.Http.Exception";


        protected IDiagnosticTraceLogger<HttpClientTracingDiagnosticProcessor> Logger { get; }
        protected IServiceProvider ServiceProvider { get; }

        protected HttpContext HttpContext { get; }
        public HttpClientTracingDiagnosticProcessor(IHttpContextAccessor httpContextAccessor, IDiagnosticTraceLogger<HttpClientTracingDiagnosticProcessor> logger)
        {
            Logger = logger;
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
            var loginfoBuilder = HttpContext.Items[DiagnosticConstant.GetItemKeyToLogBuilder(typeof(LogInfoBuilder).FullName)] as LogInfoBuilder;
            if (loginfoBuilder != null)
            {
                var loginfo = loginfoBuilder.Build();
                request.Headers.Add(HttpConstant.TRACK_ID, loginfo.TrackId);
            }




        }
        protected virtual void HttpRequestHandle(HttpRequestMessage request)
        {

        }
        protected virtual void HttpRequestStopHandle(HttpRequestMessage request)
        {

        }

        protected virtual void HttpResponseHandle(HttpResponseMessage response)
        {
            var parentLoginfoBuilder = HttpContext.Items[DiagnosticConstant.GetItemKeyToLogBuilder(typeof(LogInfoBuilder).FullName)] as LogInfoBuilder;
            if (parentLoginfoBuilder != null)
            {
                var parentLogInfo = parentLoginfoBuilder.Build();
                var loginfoBuilder = LogInfoBuilder.CreateBuilder().BuildFromLogInfo(parentLogInfo).ParentId(parentLogInfo.Id).HttpRequestMessage(response.RequestMessage)
                    .HttpResponseMessage(response).ElapsedTime(getElapsedTime(HttpContext));
                Logger.LogTrace(loginfoBuilder);

            }
        }


        protected virtual void HttpExceptionHandle(HttpRequestMessage request, Exception exception)
        {
        }
        #endregion

        #region
        private long getElapsedTime(HttpContext context)
        {
            var stopwatch = context.Items[DiagnosticConstant.GetItemKeyToLogBuilder(HttpConstant.TRACK_TIME)] as Stopwatch;
            if (stopwatch != null)
            {
                return stopwatch.ElapsedMilliseconds;
            }
            return 0;
        }
        #endregion
    }

}
