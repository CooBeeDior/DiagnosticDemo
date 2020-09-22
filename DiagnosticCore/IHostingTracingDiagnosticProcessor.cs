using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DiagnosticAdapter;
using System;

namespace DiagnosticCore
{
    public interface IHostingTracingDiagnosticProcessor
    {
        [DiagnosticName("Microsoft.AspNetCore.Hosting.HttpRequestIn.Start")]
        void HttpRequestInStart(DefaultHttpContext httpContext);

        [DiagnosticName("Microsoft.AspNetCore.Hosting.BeginRequest")]
        void BeginRequest(HttpContext httpContext);

        [DiagnosticName("Microsoft.AspNetCore.Routing.EndpointMatched")]
        void EndpointMatched(HttpContext httpContext);


        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeAction")]
        void BeforeAction(ActionDescriptor actionDescriptor, ActionExecutingContext httpContext, RouteData routeData);

        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeOnActionExecuting")]
        void BeforeOnActionExecuting(ActionDescriptor actionDescriptor, ActionExecutingContext httpContext, UnsupportedContentTypeFilter filter);

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterOnActionExecuting")]
        void AfterOnActionExecuting(ActionDescriptor actionDescriptor, ActionExecutingContext httpContext, UnsupportedContentTypeFilter filter);

        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeOnActionExecuting")]
        void BeforeOnActionExecuting(ActionDescriptor actionDescriptor, ActionExecutingContext httpContext, ModelStateInvalidFilter filter);

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterOnActionExecuting")]
        void AfterOnActionExecuting(ActionDescriptor actionDescriptor, ActionExecutingContext httpContext, ModelStateInvalidFilter filter);

        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeActionMethod")]
        void BeforeActionMethod(ActionContext actionContext);

        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeControllerActionMethod")]
        void BeforeControllerActionMethod(ActionContext actionContext);

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterControllerActionMethod")]
        void AfterControllerActionMethod(ActionContext actionContext, ObjectResult result);

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterActionMethod")]
        void AfterActionMethod(ActionContext actionContext);

        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeOnActionExecuted")]
        void EndpointMatched(ActionDescriptor actionDescriptor, ActionExecutedContext actionExecutedContext, ModelStateInvalidFilter filter);

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterOnActionExecuted")]
        void AfterOnActionExecuted(ActionDescriptor actionDescriptor, ActionExecutedContext actionExecutedContext, ModelStateInvalidFilter filter);

        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeOnActionExecuted")]
        void BeforeOnActionExecuted(ActionDescriptor actionDescriptor, ActionExecutedContext actionExecutedContext, UnsupportedContentTypeFilter filter);

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterOnActionExecuted")]
        void AfterOnActionExecuted(ActionDescriptor actionDescriptor, ActionExecutedContext actionExecutedContext, UnsupportedContentTypeFilter filter);

        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeOnResultExecuting")]
        void BeforeOnResultExecuting(ActionDescriptor actionDescriptor, ResultExecutingContext resultExecutingContext);

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterOnResultExecuting")]
        void AfterOnResultExecuting(ActionDescriptor actionDescriptor, ResultExecutingContext resultExecutingContext);

        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeActionResult")]
        void BeforeActionResult(ActionContext actionContext, ObjectResult objectResult);

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterActionResult")]
        void AfterActionResult(ActionContext actionContext, ObjectResult objectResult);

        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeOnResultExecuted")]
        void BeforeOnResultExecuted(ActionDescriptor actionDescriptor, ResultExecutedContext resultExecutedContext);

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterOnResultExecuted")]
        void AfterOnResultExecuted(ActionDescriptor actionDescriptor, ResultExecutedContext resultExecutedContext);
      
        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterAction")]
        void AfterAction(ActionDescriptor actionDescriptor, HttpContext httpContext, RouteData routeData);
        
        [DiagnosticName("Microsoft.AspNetCore.Hosting.EndRequest")]
        void EndRequest(HttpContext httpContext);

        [DiagnosticName("Microsoft.AspNetCore.Hosting.HttpRequestIn.Stop")]
        void HttpRequestInStop(HttpContext httpContext);

        [DiagnosticName("Microsoft.AspNetCore.Diagnostics.UnhandledException")]
        void DiagnosticUnhandledException(HttpContext httpContext, Exception exception);



        [DiagnosticName("Microsoft.AspNetCore.Hosting.UnhandledException")]
        void HostingUnhandledException(HttpContext httpContext, Exception exception);
        



    }
}
