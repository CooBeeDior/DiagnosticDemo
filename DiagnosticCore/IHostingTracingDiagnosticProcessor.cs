using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.DiagnosticAdapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiagnosticCore
{
    public interface IHostingTracingDiagnosticProcessor
    {

        [DiagnosticName("Microsoft.AspNetCore.Hosting.BeginRequest")]
        void BeginRequest(HttpContext httpContext);


        [DiagnosticName("Microsoft.AspNetCore.Hosting.EndRequest")]
        void EndRequest(HttpContext httpContext);


        [DiagnosticName("Microsoft.AspNetCore.Diagnostics.UnhandledException")]
        void DiagnosticUnhandledException(HttpContext httpContext, Exception exception);


        [DiagnosticName("Microsoft.AspNetCore.Hosting.UnhandledException")]
        void HostingUnhandledException(HttpContext httpContext, Exception exception);


        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeAction")]
        void BeforeAction(ActionDescriptor actionDescriptor, HttpContext httpContext);


        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterAction")]
        void AfterAction(ActionDescriptor actionDescriptor, HttpContext httpContext);
         
    }
}
