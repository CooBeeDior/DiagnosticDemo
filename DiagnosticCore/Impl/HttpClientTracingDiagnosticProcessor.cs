﻿using DiagnosticCore.Constant;
using DiagnosticCore.LogCore;
using DiagnosticModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DiagnosticAdapter;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace DiagnosticCore
{


    /// <summary>
    /// 全局HttpClient监听
    /// </summary>
    public class HttpClientTracingDiagnosticProcessor : IHttpClientTracingDiagnosticProcessor
    {
        public const string LISTENERNAME = "HttpHandlerDiagnosticListener";
        public const string HttpRequestStartName = "System.Net.Http.HttpRequestOut.Start";
        public const string HttpRequestName = "System.Net.Http.Request";
        public const string HttpRequestStopName = "System.Net.Http.HttpRequestOut.Stop";
        public const string HttpResponseName = "System.Net.Http.Response";
        public const string HttpExceptionName = "System.Net.Http.Exception";

        public string ListenerName { get { return LISTENERNAME; } }



        protected IDiagnosticTraceLogger<HttpClientTracingDiagnosticProcessor> Logger { get; }

        protected readonly IHttpContextAccessor HttpContextAccessor;

        protected readonly DiagnosticOptions Options;

        protected IServiceProvider ServiceProvider { get; }
        public HttpClientTracingDiagnosticProcessor(IHttpContextAccessor httpContextAccessor, IDiagnosticTraceLogger<HttpClientTracingDiagnosticProcessor> logger, DiagnosticOptions options, IServiceProvider serviceProvider)
        {
            Logger = logger;
            HttpContextAccessor = httpContextAccessor;
            Options = options;
            ServiceProvider = serviceProvider;
        }

        [DiagnosticName(HttpRequestStartName)]
        public void HttpRequestStart(HttpRequestMessage request)
        {
            var activity = System.Diagnostics.Activity.Current;
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


        }
        protected virtual void HttpRequestHandle(HttpRequestMessage request)
        {

            var activity1 = System.Diagnostics.Activity.Current;
            activity1?.AddTag("3", "3");
            activity1?.AddBaggage("3", "3");
            if (Options.RequestRule.Invoke(ServiceProvider, HttpContextAccessor?.HttpContext?.Request))
            {
                if (HttpContextAccessor.HttpContext != null && HttpContextAccessor.HttpContext.Items.ContainsKey(DiagnosticConstant.GetItemKey(typeof(TraceInfoBuilder).FullName)))
                {
                    var traceInfoBuilder = HttpContextAccessor.HttpContext.Items[DiagnosticConstant.GetItemKey(typeof(TraceInfoBuilder).FullName)] as TraceInfoBuilder;
                    if (traceInfoBuilder != null)
                    {
                        var traceInfo = traceInfoBuilder.Build();
                        request.Headers.Add(HttpConstant.TRACK_ID, traceInfo?.TrackId ?? "");

                    }
                }
            }

        }
        protected virtual void HttpRequestStopHandle(HttpRequestMessage request)
        {

        }

        protected virtual void HttpResponseHandle(HttpResponseMessage response)
        {
            var activity1 = System.Diagnostics.Activity.Current;
            activity1?.AddTag("4", "4");
            activity1?.AddBaggage("4", "4");
            if (Options.RequestRule.Invoke(ServiceProvider, HttpContextAccessor?.HttpContext?.Request))
            {
                string serviceName = null;
                if (response.RequestMessage.Headers.TryGetValues(HttpConstant.TRACEMICROSERVICE, out IEnumerable<string> serviceNames))
                {
                    serviceName = serviceNames?.FirstOrDefault();
                }

                if (HttpContextAccessor.HttpContext != null && HttpContextAccessor.HttpContext.Items.ContainsKey(DiagnosticConstant.GetItemKey(typeof(TraceInfoBuilder).FullName)))
                {
                    var parentTraceInfoBuilder = HttpContextAccessor.HttpContext.Items[DiagnosticConstant.GetItemKey(typeof(TraceInfoBuilder).FullName)] as TraceInfoBuilder;
                    if (parentTraceInfoBuilder != null)
                    {
                        var parentTraceInfo = parentTraceInfoBuilder.Build();
                        var traceInfoBuilder = TraceInfoBuilder.CreateBuilder().BuildFromTraceInfo(parentTraceInfo).ParentId(parentTraceInfo.Id).HttpRequestMessage(response.RequestMessage)
                            .HttpResponseMessage(response).ElapsedTime(HttpContextAccessor.HttpContext.ElapsedTime()).TargetServerName(serviceName).Log(LogLevel.Trace);

                        Logger.LogInformation(traceInfoBuilder);

                    }
                }
            }

        }


        protected virtual void HttpExceptionHandle(HttpRequestMessage request, Exception exception)
        {
            if (Options.RequestRule.Invoke(ServiceProvider, HttpContextAccessor?.HttpContext?.Request))
            {
                if (HttpContextAccessor.HttpContext != null && HttpContextAccessor.HttpContext.Items.ContainsKey(DiagnosticConstant.GetItemKey(typeof(TraceInfoBuilder).FullName)))
                {
                    var parentTraceInfoBuilder = HttpContextAccessor.HttpContext.Items[DiagnosticConstant.GetItemKey(typeof(TraceInfoBuilder).FullName)] as TraceInfoBuilder;
                    if (parentTraceInfoBuilder != null)
                    {
                        var parentTraceInfo = parentTraceInfoBuilder.Build();
                        var traceInfoBuilder = TraceInfoBuilder.CreateBuilder().BuildFromTraceInfo(parentTraceInfo).ParentId(parentTraceInfo.Id).HttpRequestMessage(request)
                             .ElapsedTime(HttpContextAccessor.HttpContext.ElapsedTime()).Exception(exception).Log(LogLevel.Trace);
                        Logger.LogInformation(traceInfoBuilder);

                    }
                }
            }
        }
        #endregion


    }

}
