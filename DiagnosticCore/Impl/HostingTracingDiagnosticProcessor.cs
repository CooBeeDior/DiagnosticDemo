using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DiagnosticAdapter;
using Microsoft.Extensions.Logging;
using MongodbCore;
using System;
using System.Linq;


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


        [DiagnosticName("Microsoft.AspNetCore.Hosting.HttpRequestIn.Start")]
        public void HttpRequestInStart(DefaultHttpContext httpContext)
        {

        }

        [DiagnosticName("Microsoft.AspNetCore.Hosting.BeginRequest")]
        public void BeginRequest(HttpContext httpContext)
        {
            BeginRequestHandle(httpContext);
        }

        [DiagnosticName("Microsoft.AspNetCore.Routing.EndpointMatched")]
        public void EndpointMatched(HttpContext httpContext)
        {
            EndpointMatchedHandle(httpContext);
        }


        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeAction")]
        public void BeforeAction(ActionDescriptor actionDescriptor, ActionExecutingContext actionExecutingContext, RouteData routeData)
        {
            BeforeActionHandle(actionDescriptor, actionExecutingContext, routeData);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeOnActionExecuting")]
        public void BeforeOnActionExecuting(ActionDescriptor actionDescriptor, ActionExecutingContext actionExecutingContext, UnsupportedContentTypeFilter filter)
        {
            BeforeOnActionExecutingHandle(actionDescriptor, actionExecutingContext, filter);

        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterOnActionExecuting")]
        public void AfterOnActionExecuting(ActionDescriptor actionDescriptor, ActionExecutingContext actionExecutingContext, UnsupportedContentTypeFilter filter)
        {
            AfterOnActionExecutingHandle(actionDescriptor, actionExecutingContext, filter);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeOnActionExecuting")]
        public void BeforeOnActionExecuting(ActionDescriptor actionDescriptor, ActionExecutingContext actionExecutingContext, ModelStateInvalidFilter filter)
        {
            BeforeOnActionExecutingHandle(actionDescriptor, actionExecutingContext, filter);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterOnActionExecuting")]
        public void AfterOnActionExecuting(ActionDescriptor actionDescriptor, ActionExecutingContext actionExecutingContext, ModelStateInvalidFilter filter)
        {
            AfterOnActionExecutingHandle(actionDescriptor, actionExecutingContext, filter);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeActionMethod")]
        public void BeforeActionMethod(ActionContext actionContext)
        {
            BeforeActionMethodHandle(actionContext);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeControllerActionMethod")]
        public void BeforeControllerActionMethod(ActionContext actionContext)
        {

            BeforeControllerActionMethodHandle(actionContext);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterControllerActionMethod")]
        public void AfterControllerActionMethod(ActionContext actionContext, ObjectResult result)
        {
            AfterControllerActionMethodHandle(actionContext, result);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterActionMethod")]
        public void AfterActionMethod(ActionContext actionContext)
        {
            AfterActionMethodHandle(actionContext);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeOnActionExecuted")]
        public void EndpointMatched(ActionDescriptor actionDescriptor, ActionExecutedContext actionExecutedContext, ModelStateInvalidFilter filter)
        {
            EndpointMatchedHandle(actionDescriptor, actionExecutedContext, filter);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterOnActionExecuted")]
        public void AfterOnActionExecuted(ActionDescriptor actionDescriptor, ActionExecutedContext actionExecutedContext, ModelStateInvalidFilter filter)
        {
            AfterOnActionExecutedHandle(actionDescriptor, actionExecutedContext, filter);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeOnActionExecuted")]
        public void BeforeOnActionExecuted(ActionDescriptor actionDescriptor, ActionExecutedContext actionExecutedContext, UnsupportedContentTypeFilter filter)
        {
            BeforeOnActionExecutedHandle(actionDescriptor, actionExecutedContext, filter);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterOnActionExecuted")]
        public void AfterOnActionExecuted(ActionDescriptor actionDescriptor, ActionExecutedContext actionExecutedContext, UnsupportedContentTypeFilter filter)
        {
            AfterOnActionExecutedHandle(actionDescriptor, actionExecutedContext, filter);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeOnResultExecuting")]
        public void BeforeOnResultExecuting(ActionDescriptor actionDescriptor, ResultExecutingContext resultExecutingContext)
        {
            BeforeOnResultExecutingHandle(actionDescriptor, resultExecutingContext);

        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterOnResultExecuting")]
        public void AfterOnResultExecuting(ActionDescriptor actionDescriptor, ResultExecutingContext resultExecutingContext)
        {
            AfterOnResultExecutingHandle(actionDescriptor, resultExecutingContext);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeActionResult")]
        public void BeforeActionResult(ActionContext actionContext, ObjectResult objectResult)
        {
            BeforeActionResultHandle(actionContext, objectResult);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterActionResult")]
        public void AfterActionResult(ActionContext actionContext, ObjectResult objectResult)
        {
            AfterActionResultHandle(actionContext, objectResult);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeOnResultExecuted")]
        public void BeforeOnResultExecuted(ActionDescriptor actionDescriptor, ResultExecutedContext resultExecutedContext)
        {
            BeforeOnResultExecutedHandle(actionDescriptor, resultExecutedContext);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterOnResultExecuted")]
        public void AfterOnResultExecuted(ActionDescriptor actionDescriptor, ResultExecutedContext resultExecutedContext)
        {
            AfterOnResultExecutedHandle(actionDescriptor, resultExecutedContext);

        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterAction")]
        public void AfterAction(ActionDescriptor actionDescriptor, HttpContext httpContext, RouteData routeData)
        {
            AfterActionHandle(actionDescriptor, httpContext, routeData);
        }

        [DiagnosticName("Microsoft.AspNetCore.Hosting.EndRequest")]
        public void EndRequest(HttpContext httpContext)
        {
            EndRequestHandle(httpContext);
        }

        [DiagnosticName("Microsoft.AspNetCore.Hosting.HttpRequestIn.Stop")]
        public void HttpRequestInStop(HttpContext httpContext)
        {
            HttpRequestInStopHandle(httpContext);
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





        #region protected  



        protected virtual void HttpRequestInStartHandle(DefaultHttpContext httpContext)
        {

        }


        protected virtual void BeginRequestHandle(HttpContext httpContext)
        {
            var request = httpContext.Request;
            var parentTrackId = request.Headers["track-id"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(parentTrackId))
            {
                request.Headers.Add("parent-track-id", parentTrackId);
            }
            request.Headers.Add("chain-id", Guid.NewGuid().ToString());
            request.Headers.Add("track-id", Guid.NewGuid().ToString());

            request.Headers.Add("track-time", DateTime.Now.Ticks.ToString());
        }


        protected virtual void EndpointMatchedHandle(HttpContext httpContext) { }



        protected virtual void BeforeActionHandle(ActionDescriptor actionDescriptor, ActionExecutingContext httpContext, RouteData routeData) { }


        protected virtual void BeforeOnActionExecutingHandle(ActionDescriptor actionDescriptor, ActionExecutingContext httpContext, UnsupportedContentTypeFilter filter) { }


        protected virtual void AfterOnActionExecutingHandle(ActionDescriptor actionDescriptor, ActionExecutingContext httpContext, UnsupportedContentTypeFilter filter) { }


        protected virtual void BeforeOnActionExecutingHandle(ActionDescriptor actionDescriptor, ActionExecutingContext httpContext, ModelStateInvalidFilter filter) { }


        protected virtual void AfterOnActionExecutingHandle(ActionDescriptor actionDescriptor, ActionExecutingContext httpContext, ModelStateInvalidFilter filter) { }


        protected virtual void BeforeActionMethodHandle(ActionContext actionContext) { }


        protected virtual void BeforeControllerActionMethodHandle(ActionContext actionContext) { }


        protected virtual void AfterControllerActionMethodHandle(ActionContext actionContext, ObjectResult result) { }


        protected virtual void AfterActionMethodHandle(ActionContext actionContext) { }


        protected virtual void EndpointMatchedHandle(ActionDescriptor actionDescriptor, ActionExecutedContext actionExecutedContext, ModelStateInvalidFilter filter) { }


        protected virtual void AfterOnActionExecutedHandle(ActionDescriptor actionDescriptor, ActionExecutedContext actionExecutedContext, ModelStateInvalidFilter filter) { }


        protected virtual void BeforeOnActionExecutedHandle(ActionDescriptor actionDescriptor, ActionExecutedContext actionExecutedContext, UnsupportedContentTypeFilter filter) { }


        protected virtual void AfterOnActionExecutedHandle(ActionDescriptor actionDescriptor, ActionExecutedContext actionExecutedContext, UnsupportedContentTypeFilter filter) { }

        protected virtual void BeforeOnResultExecutingHandle(ActionDescriptor actionDescriptor, ResultExecutingContext resultExecutingContext) { }


        protected virtual void AfterOnResultExecutingHandle(ActionDescriptor actionDescriptor, ResultExecutingContext resultExecutingContext) { }


        protected virtual void BeforeActionResultHandle(ActionContext actionContext, ObjectResult objectResult) { }


        protected virtual void AfterActionResultHandle(ActionContext actionContext, ObjectResult objectResult) { }


        protected virtual void BeforeOnResultExecutedHandle(ActionDescriptor actionDescriptor, ResultExecutedContext resultExecutedContext) { }


        protected virtual void AfterOnResultExecutedHandle(ActionDescriptor actionDescriptor, ResultExecutedContext resultExecutedContext) { }


        protected virtual void AfterActionHandle(ActionDescriptor actionDescriptor, HttpContext httpContext, RouteData routeData) { }


        protected virtual void EndRequestHandle(HttpContext httpContext)
        {
            httpContext.ToLogInfo().ToPersistence(ServiceProvider);
        }


        protected virtual void HttpRequestInStopHandle(HttpContext httpContext) { }


        protected virtual void DiagnosticUnhandledExceptionHandle(HttpContext httpContext, Exception exception)
        {
            var id = Guid.NewGuid().ToString();
            httpContext.ToLogInfo(id, exception).ToPersistence(ServiceProvider);
        }




        protected virtual void HostingUnhandledExceptionHandle(HttpContext httpContext, Exception exception)
        {
            var id = Guid.NewGuid().ToString();
            httpContext.ToLogInfo(id, exception).ToPersistence(ServiceProvider);
        }







        #endregion
    }
}
