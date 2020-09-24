using DiagnosticModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DiagnosticAdapter;
using Microsoft.Extensions.Logging;
using System;
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
 

        protected ILogger<HttpClientTracingDiagnosticProcessor> Logger { get; }
        protected IServiceProvider ServiceProvider { get; }
        public HttpClientTracingDiagnosticProcessor(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Logger = serviceProvider.GetService<ILogger<HttpClientTracingDiagnosticProcessor>>();
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

        private LogInfoBuilder _logInfoBuilder = LogInfoBuilder.CreateBuilder();
        protected virtual void HttpRequestStartHandle(HttpRequestMessage request)
        {
            _logInfoBuilder.ClearLogInfo();



        }
        protected virtual void HttpRequestHandle(HttpRequestMessage request)
        {

        }
        protected virtual void HttpRequestStopHandle(HttpRequestMessage request)
        {

        }

        protected virtual void HttpResponseHandle(HttpResponseMessage response)
        {
        }


        protected virtual void HttpExceptionHandle(HttpRequestMessage request, Exception exception)
        {
        }
        #endregion
    }

}
