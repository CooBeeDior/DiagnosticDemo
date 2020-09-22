using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DiagnosticAdapter;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;
namespace DiagnosticCore
{
    public class HttpClientTracingDiagnosticProcessor : IHttpClientTracingDiagnosticProcessor
    {
        protected ILogger<HttpClientTracingDiagnosticProcessor> Logger { get; }
        protected IServiceProvider ServiceProvider { get; }
        public HttpClientTracingDiagnosticProcessor(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Logger = serviceProvider.GetService<ILogger<HttpClientTracingDiagnosticProcessor>>();
        }


        public readonly static string ListenerName = "HttpHandlerDiagnosticListener";



        [DiagnosticName("System.Net.Http.HttpRequestOut.Start")]
        public void HttpRequestStart(HttpRequestMessage request)
        {
            HttpRequestStartHandle(request);
        }

        [DiagnosticName("System.Net.Http.Request")]
        public void HttpRequest(HttpRequestMessage request)
        {
            HttpRequestHandle(request);
        }

        [DiagnosticName("System.Net.Http.HttpRequestOut.Stop")]
        public void HttpRequestStop(HttpRequestMessage request)
        {
            HttpRequestStopHandle(request);
        }

        [DiagnosticName("System.Net.Http.Response")]
        public void HttpResponse(HttpResponseMessage response)
        {
            HttpResponseHandle(response);
        }

        [DiagnosticName("System.Net.Http.Exception")]
        public void HttpException(HttpRequestMessage request, Exception exception)
        {
            HttpExceptionHandle(request, exception);
        }
        protected virtual void HttpRequestStartHandle(HttpRequestMessage request)
        {
         
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
    }

}
