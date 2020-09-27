using DiagnosticCore.Constant;
using DiagnosticCore.LogCore;
using DiagnosticModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DiagnosticAdapter;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace DiagnosticCore
{

    public class HostingTracingDiagnosticProcessor : IHostingTracingDiagnosticProcessor
    {
        public readonly static string ListenerName = "Microsoft.AspNetCore";

        public const string HttpRequestInStartName = "Microsoft.AspNetCore.Hosting.HttpRequestIn.Start";
        public const string BeginRequestName = "Microsoft.AspNetCore.Hosting.BeginRequest";
        public const string EndpointMatchedName = "Microsoft.AspNetCore.Routing.EndpointMatched";
        public const string BeforeActionName = "Microsoft.AspNetCore.Mvc.BeforeAction";
        public const string BeforeOnActionExecutingName = "Microsoft.AspNetCore.Mvc.BeforeOnActionExecuting";
        public const string AfterOnActionExecutingName = "Microsoft.AspNetCore.Mvc.AfterOnActionExecuting";
        public const string BeforeActionMethodName = "Microsoft.AspNetCore.Mvc.BeforeActionMethod";
        public const string BeforeControllerActionMethodName = "Microsoft.AspNetCore.Mvc.BeforeControllerActionMethod";
        public const string AfterControllerActionMethodName = "Microsoft.AspNetCore.Mvc.AfterControllerActionMethod";
        public const string AfterActionMethodName = "Microsoft.AspNetCore.Mvc.AfterActionMethod";
        public const string BeforeOnActionExecutedName = "Microsoft.AspNetCore.Mvc.BeforeOnActionExecuted";
        public const string AfterOnActionExecutedName = "Microsoft.AspNetCore.Mvc.AfterOnActionExecuted";
        public const string BeforeOnResultExecutingName = "Microsoft.AspNetCore.Mvc.BeforeOnResultExecuting";
        public const string AfterOnResultExecutingName = "Microsoft.AspNetCore.Mvc.AfterOnResultExecuting";
        public const string BeforeActionResultName = "Microsoft.AspNetCore.Mvc.BeforeActionResult";
        public const string AfterActionResultName = "Microsoft.AspNetCore.Mvc.AfterActionResult";
        public const string BeforeOnResultExecutedName = "Microsoft.AspNetCore.Mvc.BeforeOnResultExecuted";
        public const string AfterOnResultExecutedName = "Microsoft.AspNetCore.Mvc.AfterOnResultExecuted";
        public const string AfterActionName = "Microsoft.AspNetCore.Mvc.AfterAction";
        public const string EndRequestName = "Microsoft.AspNetCore.Hosting.EndRequest";
        public const string HttpRequestInStopName = "Microsoft.AspNetCore.Hosting.HttpRequestIn.Stop";
        public const string DiagnosticsUnhandledExceptionName = "Microsoft.AspNetCore.Diagnostics.UnhandledException";
        public const string HostingUnhandledExceptionName = "Microsoft.AspNetCore.Hosting.UnhandledException";


        protected IDiagnosticTraceLogger<HostingTracingDiagnosticProcessor> Logger { get; }



        public HostingTracingDiagnosticProcessor(IDiagnosticTraceLogger<HostingTracingDiagnosticProcessor> logger)
        { 
            Logger = logger; 
        }


        [DiagnosticName(HttpRequestInStartName)]
        public void HttpRequestInStart(DefaultHttpContext httpContext)
        {

        }

        [DiagnosticName(BeginRequestName)]
        public void BeginRequest(HttpContext httpContext)
        {
            BeginRequestHandle(httpContext);
        }

        [DiagnosticName(EndpointMatchedName)]
        public void EndpointMatched(HttpContext httpContext)
        {
            EndpointMatchedHandle(httpContext);
        }


        [DiagnosticName(BeforeActionName)]
        public void BeforeAction(ActionDescriptor actionDescriptor, RouteData routeData)
        {
            BeforeActionHandle(actionDescriptor, routeData);
        }



        [DiagnosticName(BeforeOnActionExecutingName)]
        public void BeforeOnActionExecuting(ActionDescriptor actionDescriptor, ActionExecutingContext actionExecutingContext)
        {
            BeforeOnActionExecutingHandle(actionDescriptor, actionExecutingContext);
        }

        [DiagnosticName(AfterOnActionExecutingName)]
        public void AfterOnActionExecuting(ActionDescriptor actionDescriptor, ActionExecutingContext actionExecutingContext)
        {
            AfterOnActionExecutingHandle(actionDescriptor, actionExecutingContext);
        }

        [DiagnosticName(BeforeActionMethodName)]
        public void BeforeActionMethod(ActionContext actionContext)
        {
            BeforeActionMethodHandle(actionContext);
        }

        [DiagnosticName(BeforeControllerActionMethodName)]
        public void BeforeControllerActionMethod(ActionContext actionContext)
        {

            BeforeControllerActionMethodHandle(actionContext);
        }

        [DiagnosticName(AfterControllerActionMethodName)]
        public void AfterControllerActionMethod(ActionContext actionContext, ObjectResult result)
        {
            AfterControllerActionMethodHandle(actionContext, result);
        }

        [DiagnosticName(AfterActionMethodName)]
        public void AfterActionMethod(ActionContext actionContext)
        {
            AfterActionMethodHandle(actionContext);
        }

        [DiagnosticName(BeforeOnActionExecutedName)]
        public void BeforeOnActionExecuted(ActionDescriptor actionDescriptor, ActionExecutedContext actionExecutedContext)
        {
            BeforeOnActionExecutedHandle(actionDescriptor, actionExecutedContext);
        }




        [DiagnosticName(AfterOnActionExecutedName)]
        public void AfterOnActionExecuted(ActionDescriptor actionDescriptor, ActionExecutedContext actionExecutedContext)
        {
            AfterOnActionExecutedHandle(actionDescriptor, actionExecutedContext);
        }


        [DiagnosticName(BeforeOnResultExecutingName)]
        public void BeforeOnResultExecuting(ActionDescriptor actionDescriptor, ResultExecutingContext resultExecutingContext)
        {
            BeforeOnResultExecutingHandle(actionDescriptor, resultExecutingContext);

        }

        [DiagnosticName(AfterOnResultExecutingName)]
        public void AfterOnResultExecuting(ActionDescriptor actionDescriptor, ResultExecutingContext resultExecutingContext)
        {
            AfterOnResultExecutingHandle(actionDescriptor, resultExecutingContext);

        }

        [DiagnosticName(BeforeActionResultName)]
        public void BeforeActionResult(ActionContext actionContext)
        {
            BeforeActionResultHandle(actionContext);
        }

        [DiagnosticName(AfterActionResultName)]
        public void AfterActionResult(ActionContext actionContext)
        {
            AfterActionResultHandle(actionContext);
        }

        [DiagnosticName(BeforeOnResultExecutedName)]
        public void BeforeOnResultExecuted(ActionDescriptor actionDescriptor, ResultExecutedContext resultExecutedContext)
        {
            BeforeOnResultExecutedHandle(actionDescriptor, resultExecutedContext);
        }

        [DiagnosticName(AfterOnResultExecutedName)]
        public void AfterOnResultExecuted(ActionDescriptor actionDescriptor, ResultExecutedContext resultExecutedContext)
        {
            AfterOnResultExecutedHandle(actionDescriptor, resultExecutedContext);

        }



        [DiagnosticName(AfterActionName)]
        public void AfterAction(ActionDescriptor actionDescriptor, HttpContext httpContext, RouteData routeData)
        {
            AfterActionHandle(actionDescriptor, httpContext, routeData);
        }

        [DiagnosticName(EndRequestName)]
        public void EndRequest(HttpContext httpContext)
        {
            EndRequestHandle(httpContext);
        }

        [DiagnosticName(HttpRequestInStopName)]
        public void HttpRequestInStop(HttpContext httpContext)
        {
            HttpRequestInStopHandle(httpContext);
        }

        [DiagnosticName(DiagnosticsUnhandledExceptionName)]
        public void DiagnosticUnhandledException(HttpContext httpContext, Exception exception)
        {
            DiagnosticUnhandledExceptionHandle(httpContext, exception);
        }



        [DiagnosticName(HostingUnhandledExceptionName)]
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
            var parentTrackId = request.Headers[HttpConstant.TRACK_ID].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(parentTrackId))
            {
                request.Headers.Add(HttpConstant.PARENT_TRACK_ID, parentTrackId);
            }
            var trackId = Guid.NewGuid().ToString();
            request.Headers.Add(HttpConstant.TRACK_ID, trackId);

            //当前服务追踪的Id
            var chainId = Guid.NewGuid().ToString();
            request.Headers.Add(HttpConstant.CHAIN_ID, chainId);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            httpContext.Items.Add(DiagnosticConstant.GetItemKeyToLogBuilder(HttpConstant.TRACK_TIME), stopwatch);
            var logInfoBuilder = LogInfoBuilder.CreateBuilder().BuildLogInfo(chainId).TrackId(trackId, parentTrackId).HttpContext(httpContext);
            httpContext.Items.Add(DiagnosticConstant.GetItemKeyToLogBuilder(typeof(LogInfoBuilder).FullName), logInfoBuilder);

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

            var builder = resultExecutedContext.HttpContext.Items[DiagnosticConstant.GetItemKeyToLogBuilder(typeof(LogInfoBuilder).FullName)];
            if (builder != null && builder is LogInfoBuilder logInfoBuilder && resultExecutedContext.Result != null)
            {
                setLogResponse(resultExecutedContext.Result, logInfoBuilder);

            }
        }


        protected virtual void AfterActionHandle(ActionDescriptor actionDescriptor, HttpContext httpContext, RouteData routeData)
        {

        }


        protected virtual void EndRequestHandle(HttpContext httpContext)
        {
            var builder = httpContext.Items[DiagnosticConstant.GetItemKeyToLogBuilder(typeof(LogInfoBuilder).FullName)];
            if (builder != null && builder is LogInfoBuilder logInfoBuilder)
            {
                var elapsedTime = getElapsedTime(httpContext);
                logInfoBuilder.ElapsedTime(elapsedTime);
                Logger.LogTrace(logInfoBuilder);


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
            var builder = httpContext.Items[DiagnosticConstant.GetItemKeyToLogBuilder(typeof(LogInfoBuilder).FullName)];
            if (builder != null && builder is LogInfoBuilder logInfoBuilder)
            {
                var elapsedTime = getElapsedTime(httpContext);
                var loginfo = logInfoBuilder.Build();
                var logInfoBuilderNew = LogInfoBuilder.CreateBuilder().BuildFromLogInfo(loginfo).ParentId(loginfo.Id)
                    .HttpContext(httpContext).ElapsedTime(elapsedTime);
                return logInfoBuilderNew;
            }
            return null;
        }

        private void setLogResponse(IActionResult actionResult, LogInfoBuilder logInfoBuilder)
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
                var property = actionResult.GetType().GetProperty(HttpConstant.StatusCode);
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
