using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DiagnosticAdapter;
using Microsoft.Extensions.Logging;
using System;
namespace DiagnosticCore
{

    public class HostingTracingDiagnosticProcessor : IHostingTracingDiagnosticProcessor
    {
        public readonly static string ListenerName = "Microsoft.AspNetCore";
        protected ILogger<HostingTracingDiagnosticProcessor> Logger { get; }
        protected IServiceProvider ServiceProvider { get; }


        public HostingTracingDiagnosticProcessor(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Logger = serviceProvider.GetService<ILogger<HostingTracingDiagnosticProcessor>>();
        }



        [DiagnosticName("Microsoft.AspNetCore.Hosting.BeginRequest")]
        public void BeginRequest(HttpContext httpContext)
        {
            BeginRequestHandle(httpContext);
        }

        [DiagnosticName("Microsoft.AspNetCore.Hosting.EndRequest")]
        public void EndRequest(HttpContext httpContext)
        {
            EndRequestHandle(httpContext);
        }

        [DiagnosticName("Microsoft.AspNetCore.Diagnostics.UnhandledException")]
        public void DiagnosticUnhandledException(HttpContext httpContext, Exception exception)
        {
            DiagnosticUnhandledExceptionHandle(httpContext, exception);
        }

        [DiagnosticName("Microsoft.AspNetCore.Hosting.UnhandledException")]
        public void HostingUnhandledException(HttpContext httpContext, Exception exception)
        {
            HostingUnhandledExceptionHandle(httpContext, exception);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeAction")]
        public void BeforeAction(ActionDescriptor actionDescriptor, HttpContext httpContext)
        {
            BeforeActionHandle(actionDescriptor, httpContext);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterAction")]
        public void AfterAction(ActionDescriptor actionDescriptor, HttpContext httpContext)
        {
            AfterActionHandle(actionDescriptor, httpContext);
        }


        #region protected      
        protected void BeginRequestHandle(HttpContext httpContext)
        {
        }


        protected void EndRequestHandle(HttpContext httpContext)
        {
        }


        protected void DiagnosticUnhandledExceptionHandle(HttpContext httpContext, Exception exception)
        {
        }


        protected void HostingUnhandledExceptionHandle(HttpContext httpContext, Exception exception)
        {
        }


        protected void BeforeActionHandle(ActionDescriptor actionDescriptor, HttpContext httpContext)
        {
        }


        protected void AfterActionHandle(ActionDescriptor actionDescriptor, HttpContext httpContext)
        {
        }
        #endregion
    }
}
