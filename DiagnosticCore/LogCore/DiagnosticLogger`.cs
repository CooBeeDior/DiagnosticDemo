using DiagnosticModel;
using MessageQueueAbstraction;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Text;

namespace DiagnosticCore.LogCore
{
    public class DiagnosticLogger : ILogger
    {
        private readonly string _categoryName;

        private readonly IRabbitmqChannelManagement _rabbitmqChannelManagement;
        public DiagnosticLogger(string categoryName, IServiceProvider serviceProvider)
        {
            _categoryName = categoryName;
            _rabbitmqChannelManagement = serviceProvider.GetService<IRabbitmqChannelManagement>();

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
            if (eventId == DiagnosticConstant.EVENT_ID)
            {
                TraceInfo traceInfo = null;
                if (state is string)
                {
                    var str = state as string;
                    traceInfo = str.ToObj<TraceInfo>();

                }
                else if (state is TraceInfo)
                {
                    traceInfo = state as TraceInfo;
                }
                if (traceInfo != null)
                {
                    if (exception != null)
                    {
                        traceInfo.Exception = exception;

                    }
                    traceInfo.ErrorMessage = traceInfo.Exception?.Message;

                    //通过异步发送TraceInfo
                    var buffer = Encoding.UTF8.GetBytes(traceInfo.ToJson());
                    var model = _rabbitmqChannelManagement.GetChannel(TraceLogRabbitmqConsumer.NAME);
                    model.BasicPublish("", TraceLogRabbitmqConsumer.NAME, null, buffer);

                }

            }
            else
            {
                //TraceInfoBuilder.CreateBuilder(). 
                //model.BasicPublish("", TraceLogRabbitmqConsumer.NAME, null, buffer);
            }

        }
    }
}
