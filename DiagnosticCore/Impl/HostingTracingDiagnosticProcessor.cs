using DiagnosticCore.LogCore;
using DiagnosticCore.Models;
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
using System.Net;

namespace DiagnosticCore
{

    public class HostingTracingDiagnosticProcessor : IHostingTracingDiagnosticProcessor
    {
        public readonly static string ListenerName = "Microsoft.AspNetCore";
        protected IServiceProvider ServiceProvider { get; }
        protected IDiagnosticTraceLogger<HostingTracingDiagnosticProcessor> Logger { get; }



        public HostingTracingDiagnosticProcessor(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Logger = serviceProvider.GetService<IDiagnosticTraceLogger<HostingTracingDiagnosticProcessor>>();

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
        public void BeforeAction(ActionDescriptor actionDescriptor, RouteData routeData)
        {
            BeforeActionHandle(actionDescriptor, routeData);
        }



        [DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeOnActionExecuting")]
        public void BeforeOnActionExecuting(ActionDescriptor actionDescriptor, ActionExecutingContext actionExecutingContext)
        {
            BeforeOnActionExecutingHandle(actionDescriptor, actionExecutingContext);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterOnActionExecuting")]
        public void AfterOnActionExecuting(ActionDescriptor actionDescriptor, ActionExecutingContext actionExecutingContext)
        {
            AfterOnActionExecutingHandle(actionDescriptor, actionExecutingContext);
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
        public void BeforeOnActionExecuted(ActionDescriptor actionDescriptor, ActionExecutedContext actionExecutedContext)
        {
            BeforeOnActionExecutedHandle(actionDescriptor, actionExecutedContext);
        }




        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterOnActionExecuted")]
        public void AfterOnActionExecuted(ActionDescriptor actionDescriptor, ActionExecutedContext actionExecutedContext)
        {
            AfterOnActionExecutedHandle(actionDescriptor, actionExecutedContext);
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
        public void BeforeActionResult(ActionContext actionContext)
        {
            BeforeActionResultHandle(actionContext);
        }

        [DiagnosticName("Microsoft.AspNetCore.Mvc.AfterActionResult")]
        public void AfterActionResult(ActionContext actionContext)
        {
            AfterActionResultHandle(actionContext);
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
        //private LogInfoBuilder _logInfoBuilder = LogInfoBuilder.CreateBuilder();


        protected virtual void HttpRequestInStartHandle(DefaultHttpContext httpContext)
        {

        }


        protected virtual void BeginRequestHandle(HttpContext httpContext)
        {
            var request = httpContext.Request;
            //上一个服务传过来 是父级的跟踪Id
            var parentTrackId = request.Headers["track-id"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(parentTrackId))
            {
                request.Headers.Add("parent-track-id", parentTrackId);
            }
            var trackId = Guid.NewGuid().ToString();
            request.Headers.Add("track-id", trackId);

            //当前服务追踪的Id
            var chainId = Guid.NewGuid().ToString();
            request.Headers.Add("chain-id", chainId);
            request.Headers.Add("track-time", DateTime.Now.Ticks.ToString());
            var logInfoBuilder = LogInfoBuilder.CreateBuilder().BuildLogInfo(chainId).TrackId(trackId, parentTrackId).HttpContext(httpContext);
            httpContext.Items.Add(DiagnosticConstant.GetItemKeyToLogBuilder(this.GetType().FullName), logInfoBuilder);

        }


        protected virtual void EndpointMatchedHandle(HttpContext httpContext) { }



        protected virtual void BeforeActionHandle(ActionDescriptor actionDescriptor, RouteData routeData) { }


        protected virtual void BeforeOnActionExecutingHandle(ActionDescriptor actionDescriptor, ActionExecutingContext httpContext) { }


        protected virtual void AfterOnActionExecutingHandle(ActionDescriptor actionDescriptor, ActionExecutingContext httpContext) { }




        protected virtual void BeforeActionMethodHandle(ActionContext actionContext) { }


        protected virtual void BeforeControllerActionMethodHandle(ActionContext actionContext) { }


        protected virtual void AfterControllerActionMethodHandle(ActionContext actionContext, ObjectResult result) { }


        protected virtual void AfterActionMethodHandle(ActionContext actionContext) { }


        protected virtual void BeforeOnActionExecutedHandle(ActionDescriptor actionDescriptor, ActionExecutedContext actionExecutedContext) { }




        protected virtual void AfterOnActionExecutedHandle(ActionDescriptor actionDescriptor, ActionExecutedContext actionExecutedContext) { }

        protected virtual void BeforeOnResultExecutingHandle(ActionDescriptor actionDescriptor, ResultExecutingContext resultExecutingContext) { }


        protected virtual void AfterOnResultExecutingHandle(ActionDescriptor actionDescriptor, ResultExecutingContext resultExecutingContext)
        {

        }


        protected virtual void BeforeActionResultHandle(ActionContext actionContext) { }


        protected virtual void AfterActionResultHandle(ActionContext actionContext) { }


        protected virtual void BeforeOnResultExecutedHandle(ActionDescriptor actionDescriptor, ResultExecutedContext resultExecutedContext) { }


        protected virtual void AfterOnResultExecutedHandle(ActionDescriptor actionDescriptor, ResultExecutedContext resultExecutedContext)
        {

            var builder = resultExecutedContext.HttpContext.Items[DiagnosticConstant.GetItemKeyToLogBuilder(this.GetType().FullName)];
            if (builder != null && builder is LogInfoBuilder logInfoBuilder && resultExecutedContext.Result != null)
            {
                setActionResult(resultExecutedContext.Result, logInfoBuilder);

            }
        }


        protected virtual void AfterActionHandle(ActionDescriptor actionDescriptor, HttpContext httpContext, RouteData routeData)
        {

        }


        protected virtual void EndRequestHandle(HttpContext httpContext)
        {
            var builder = httpContext.Items[DiagnosticConstant.GetItemKeyToLogBuilder(this.GetType().FullName)];
            if (builder != null && builder is LogInfoBuilder logInfoBuilder)
            {
                var request = httpContext.Request;
                var elapsedTime = getElapsedTime(request);
                logInfoBuilder.ElapsedTime(elapsedTime);
                Logger.LogInformation(logInfoBuilder);


            }
        }


        protected virtual void HttpRequestInStopHandle(HttpContext httpContext) { }


        protected virtual void DiagnosticUnhandledExceptionHandle(HttpContext httpContext, Exception exception)
        {
            var logInfobuilder = createErrorLogBuilder(httpContext);
            if (logInfobuilder != null)
            {
                Logger.LogError(logInfobuilder, exception);
            }

        }




        protected virtual void HostingUnhandledExceptionHandle(HttpContext httpContext, Exception exception)
        {
            var logInfobuilder = createErrorLogBuilder(httpContext);
            if (logInfobuilder != null)
            {
                Logger.LogError(logInfobuilder, exception);
            }
        }







        #endregion

        #region private

        private LogInfoBuilder createErrorLogBuilder(HttpContext httpContext)
        {
            var builder = httpContext.Items[DiagnosticConstant.GetItemKeyToLogBuilder(this.GetType().FullName)];
            if (builder != null && builder is LogInfoBuilder logInfoBuilder)
            {
                var request = httpContext.Request;
                var elapsedTime = getElapsedTime(request);
                var loginfo = logInfoBuilder.Build();
                var logInfoBuilderNew = LogInfoBuilder.CreateBuilder().BuildFromLogInfo(loginfo).ParentId(loginfo.Id).ChangeId(Guid.NewGuid().ToString())
                    .HttpContext(httpContext).ElapsedTime(elapsedTime);
                return logInfoBuilderNew;
            }
            return null;
        }

        private void setActionResult(IActionResult actionResult, LogInfoBuilder logInfoBuilder)
        { 
            if (actionResult is AntiforgeryValidationFailedResult antiforgeryValidationFailedResult)
            {
                logInfoBuilder.Response(antiforgeryValidationFailedResult?.ToJson()).StatusCode(antiforgeryValidationFailedResult.StatusCode);
            }
            else if (actionResult is ContentResult contentResult)
            {
                logInfoBuilder.Response(contentResult?.Content).StatusCode(contentResult.StatusCode);
            }
            else if (actionResult is JsonResult jsonResult)
            {
                logInfoBuilder.Response(jsonResult?.Value?.ToJson()).StatusCode(jsonResult.StatusCode);
            }
            else if (actionResult is ObjectResult objectResult)
            {
                logInfoBuilder.Response(objectResult?.Value?.ToJson()).StatusCode(objectResult.StatusCode);

            }
            else if (actionResult is PartialViewResult partialViewResult)
            {
                logInfoBuilder.Response(partialViewResult?.ToJson()).StatusCode(partialViewResult.StatusCode);
            }
            else if (actionResult is RedirectResult redirectResult)
            {
                logInfoBuilder.Response(redirectResult?.ToJson()).StatusCode((int)HttpStatusCode.Redirect);
            }
            else if (actionResult is RedirectToActionResult redirectToActionResult)
            {
                logInfoBuilder.Response(redirectToActionResult?.ToJson()).StatusCode((int)HttpStatusCode.RedirectMethod);
            }
            else if (actionResult is RedirectToPageResult redirectToPageResult)
            {
                logInfoBuilder.Response(redirectToPageResult?.ToJson()).StatusCode((int)HttpStatusCode.Redirect);
            }
            else if (actionResult is RedirectToRouteResult redirectToRouteResult)
            {
                logInfoBuilder.Response(redirectToRouteResult?.ToJson()).StatusCode((int)HttpStatusCode.RedirectMethod);
            }
            else if (actionResult is StatusCodeResult statusCodeResult)
            {
              
                logInfoBuilder.Response(statusCodeResult?.ToJson()).StatusCode(statusCodeResult.StatusCode);
            }
            else if (actionResult is ViewComponentResult viewComponentResult)
            {

                logInfoBuilder.Response(viewComponentResult?.ToJson()).StatusCode(viewComponentResult.StatusCode);
            }
            else if (actionResult is ViewResult viewResult)
            {

                logInfoBuilder.Response(viewResult?.ToJson()).StatusCode(viewResult.StatusCode);
            }
            else
            {
                var property = actionResult.GetType().GetProperty("StatusCode");
                int statuscode;
                if (property != null)
                {
                    var code = property.GetValue(actionResult);
                    if (code != null)
                    {
                        if (int.TryParse(code.ToString(), out statuscode))
                        {
                            logInfoBuilder.StatusCode(statuscode);
                        }
                    }
               ;
                }
                logInfoBuilder.Response(actionResult?.ToJson());
            }


        }

        private long getElapsedTime(HttpRequest request)
        {
            var trackTime = request.Headers["track-time"].FirstOrDefault();
            if (trackTime != null)
            {
                long endtime;
                if (long.TryParse(trackTime, out endtime))
                {
                    long elapsedTime = (DateTime.Now.Ticks - endtime) / 1000000;

                    return elapsedTime;
                }

            }
            return 0;
        }
        #endregion


    }
}
