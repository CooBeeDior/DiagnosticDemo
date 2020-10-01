using DiagnosticModel;
using MessageQueueAbstraction;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace DiagnosticCore.LogCore
{
    public class DiagnosticLogger : ILogger
    {
        private readonly string _categoryName;

        private readonly IRabbitmqChannelManagement _rabbitmqChannelManagement;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DiagnosticLogger(string categoryName, IServiceProvider serviceProvider)
        {
            _categoryName = categoryName;
            _rabbitmqChannelManagement = serviceProvider.GetService<IRabbitmqChannelManagement>();
            _httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();

        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            TraceInfo traceInfo = null;
            if (eventId == DiagnosticConstant.EVENT_ID)
            {
                if (state is string)
                {
                    var str = state as string;
                    traceInfo = str.ToObj<TraceInfo>();
                }
                else if (state is TraceInfo)
                {
                    traceInfo = state as TraceInfo;
                }
                else if (state is TraceInfoBuilder)
                {
                    var builder = state as TraceInfoBuilder;
                    traceInfo = builder?.Build();
                }


            }
            else
            {
                try
                {
                    //if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Items.ContainsKey(DiagnosticConstant.GetItemKey(typeof(TraceInfoBuilder).FullName)))
                    //{
                    //    var parentTraceInfoBuilder = _httpContextAccessor.HttpContext.Items[DiagnosticConstant.GetItemKey(typeof(TraceInfoBuilder).FullName)] as TraceInfoBuilder;
                    //    if (parentTraceInfoBuilder != null)
                    //    {
                    //        var parentTraceInfo = parentTraceInfoBuilder.Build();
                    //        var traceInfoBuilder = TraceInfoBuilder.CreateBuilder().BuildTraceInfo(Guid.NewGuid().ToString()).ParentId(parentTraceInfo.Id).
                    //            TrackId(parentTraceInfo.TrackId).ParentTrackId(parentTraceInfo.ParentTrackId)
                    //            .Log(logLevel, _categoryName, exception).Description(state?.ToString());

                    //        traceInfo = traceInfoBuilder.Build();

                    //    }


                    //}
                }
                catch (Exception ex)
                {
                }
            }

            //通过异步发送TraceInfo
            if (traceInfo != null)
            {
                var buffer = Encoding.UTF8.GetBytes(traceInfo.ToJson());
                var model = _rabbitmqChannelManagement.GetChannel(TraceLogRabbitmqConsumer.NAME);
                model.BasicPublish("", TraceLogRabbitmqConsumer.NAME, null, buffer);
            }

        }

    }
}
