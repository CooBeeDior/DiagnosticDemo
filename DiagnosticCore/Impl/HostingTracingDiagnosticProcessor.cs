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
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace DiagnosticCore
{
    /// <summary>
    /// 全局Host服务监听
    /// </summary>
    public class HostingTracingDiagnosticProcessor : IHostingTracingDiagnosticProcessor
    {
        public readonly static string LISTENERNAME = "Microsoft.AspNetCore";

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

        public string ListenerName { get { return LISTENERNAME; } }

        protected IDiagnosticTraceLogger<HostingTracingDiagnosticProcessor> Logger { get; }

        protected DiagnosticOptions Options { get; } 
        protected IServiceProvider ServiceProvider { get; }
        public HostingTracingDiagnosticProcessor(IServiceProvider serviceProvider, IDiagnosticTraceLogger<HostingTracingDiagnosticProcessor> logger, DiagnosticOptions options)
        {
            Logger = logger;
            Options = options;
            ServiceProvider = serviceProvider;
        }


        [DiagnosticName(HttpRequestInStartName)]
        public void HttpRequestInStart(DefaultHttpContext httpContext)
        {
            HttpRequestInStartHandle(httpContext);
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

        protected virtual void HttpRequestInStartHandle(DefaultHttpContext httpContext)
        {
            var activity1 = System.Diagnostics.Activity.Current;
            //本服务串联
            var spanId = activity1.SpanId;
            //服务器串联
            var traceId = activity1.TraceId;
        }


        protected virtual void BeginRequestHandle(HttpContext httpContext)
        {
            if (Options.RequestRule.Invoke(ServiceProvider,httpContext.Request))
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
                httpContext.Items.Add(DiagnosticConstant.GetItemKey(HttpConstant.TRACK_TIME), stopwatch);
                var traceInfoBuilder = TraceInfoBuilder.CreateBuilder().BuildTraceInfo(chainId).TrackId(trackId, parentTrackId).HttpContext(httpContext);
                httpContext.Items.Add(DiagnosticConstant.GetItemKey(typeof(TraceInfoBuilder).FullName), traceInfoBuilder);


            }

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
            var activity1 = System.Diagnostics.Activity.Current;
            activity1?.AddTag("7", "7");
            activity1?.AddBaggage("7", "7");
            if (Options.RequestRule.Invoke(ServiceProvider,resultExecutedContext.HttpContext.Request))
            {
                var builder = resultExecutedContext.HttpContext.Items[DiagnosticConstant.GetItemKey(typeof(TraceInfoBuilder).FullName)];
                if (builder != null && builder is TraceInfoBuilder traceInfoBuilder && resultExecutedContext.Result != null)
                {
                    setLogResponse(resultExecutedContext.Result, traceInfoBuilder);

                }
            }
        }


        protected virtual void AfterActionHandle(ActionDescriptor actionDescriptor, HttpContext httpContext, RouteData routeData)
        {

        }


        protected virtual void EndRequestHandle(HttpContext httpContext)
        {
            var builder = httpContext.Items[DiagnosticConstant.GetItemKey(typeof(TraceInfoBuilder).FullName)];
            if (builder != null && builder is TraceInfoBuilder traceInfoBuilder)
            {
                var elapsedTime = httpContext.ElapsedTime();
                traceInfoBuilder.ElapsedTime(elapsedTime);
                traceInfoBuilder.Log(LogLevel.Trace);
                Logger.LogInformation(traceInfoBuilder);


            }
        }


        protected virtual void HttpRequestInStopHandle(HttpContext httpContext) { }


        protected virtual void DiagnosticUnhandledExceptionHandle(HttpContext httpContext, Exception exception)
        {
            var traceInfoBuilder = createErrorLogBuilder(httpContext);
            if (traceInfoBuilder != null)
            {
                Logger.LogError(traceInfoBuilder, exception);
            }

        }




        protected virtual void HostingUnhandledExceptionHandle(HttpContext httpContext, Exception exception)
        {
            var traceInfoBuilder = createErrorLogBuilder(httpContext);
            if (traceInfoBuilder != null)
            {
                Logger.LogError(traceInfoBuilder, exception);
            }
        }







        #endregion

        #region private

        private TraceInfoBuilder createErrorLogBuilder(HttpContext httpContext)
        {
            var builder = httpContext.Items[DiagnosticConstant.GetItemKey(typeof(TraceInfoBuilder).FullName)];
            if (builder != null && builder is TraceInfoBuilder traceInfoBuilder)
            {
                var elapsedTime = httpContext.ElapsedTime();
                var traceInfo = traceInfoBuilder.Build();
                var traceInfoBuilderNew = TraceInfoBuilder.CreateBuilder().BuildFromTraceInfo(traceInfo).ParentId(traceInfo.Id)
                    .HttpContext(httpContext).ElapsedTime(elapsedTime);
                return traceInfoBuilderNew;
            }
            return null;
        }

        private void setLogResponse(IActionResult actionResult, TraceInfoBuilder traceInfoBuilder)
        {
            if (actionResult is AntiforgeryValidationFailedResult antiforgeryValidationFailedResult)
            {
                traceInfoBuilder.Response(antiforgeryValidationFailedResult?.ToJson()).StatusCode(antiforgeryValidationFailedResult.StatusCode);
            }
            else if (actionResult is ContentResult contentResult)
            {
                traceInfoBuilder.Response(contentResult?.Content).StatusCode(contentResult.StatusCode ?? 200);
            }
            else if (actionResult is JsonResult jsonResult)
            {
                traceInfoBuilder.Response(jsonResult?.Value?.ToJson()).StatusCode(jsonResult.StatusCode ?? 200);
            }
            else if (actionResult is ObjectResult objectResult)
            {
                traceInfoBuilder.Response(objectResult?.Value?.ToJson()).StatusCode(objectResult.StatusCode ?? 200);

            }
            else if (actionResult is PartialViewResult partialViewResult)
            {
                traceInfoBuilder.Response(partialViewResult?.ToJson()).StatusCode(partialViewResult.StatusCode ?? 200);
            }
            else if (actionResult is RedirectResult redirectResult)
            {
                traceInfoBuilder.Response(redirectResult?.ToJson()).StatusCode((int)HttpStatusCode.Redirect);
            }
            else if (actionResult is RedirectToActionResult redirectToActionResult)
            {
                traceInfoBuilder.Response(redirectToActionResult?.ToJson()).StatusCode((int)HttpStatusCode.RedirectMethod);
            }
            else if (actionResult is RedirectToPageResult redirectToPageResult)
            {
                traceInfoBuilder.Response(redirectToPageResult?.ToJson()).StatusCode((int)HttpStatusCode.Redirect);
            }
            else if (actionResult is RedirectToRouteResult redirectToRouteResult)
            {
                traceInfoBuilder.Response(redirectToRouteResult?.ToJson()).StatusCode((int)HttpStatusCode.RedirectMethod);
            }
            else if (actionResult is StatusCodeResult statusCodeResult)
            {

                traceInfoBuilder.Response(statusCodeResult?.ToJson()).StatusCode(statusCodeResult.StatusCode);
            }
            else if (actionResult is ViewComponentResult viewComponentResult)
            {

                traceInfoBuilder.Response(viewComponentResult?.ToJson()).StatusCode(viewComponentResult.StatusCode ?? 200);
            }
            else if (actionResult is ViewResult viewResult)
            {

                traceInfoBuilder.Response(viewResult?.ToJson()).StatusCode(viewResult.StatusCode ?? 200);
            }
            else
            {
                var property = actionResult.GetType().GetProperty(HttpConstant.StatusCode);
                int statuscode = 200;
                if (property != null)
                {
                    var code = property.GetValue(actionResult);
                    if (code != null)
                    {
                        if (int.TryParse(code.ToString(), out statuscode))
                        {
                            traceInfoBuilder.StatusCode(statuscode);
                        }
                    }

                }
                traceInfoBuilder.Response(actionResult?.ToJson()).StatusCode(statuscode);
            }


        }


        #endregion


    }
}
