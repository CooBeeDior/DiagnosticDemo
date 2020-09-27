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
                LogInfo logInfo = null;
                if (state is string)
                {
                    var str = state as string;
                    logInfo = str.ToObj<LogInfo>();

                }
                else if (state is LogInfo)
                {
                    logInfo = state as LogInfo;
                }
                if (logInfo != null)
                {
                    if (exception != null)
                    {
                        logInfo.Exception = exception;

                    }
                    logInfo.ErrorMessage = logInfo.Exception?.Message;

                    //通过异步发送LogInfo
                    var buffer = Encoding.UTF8.GetBytes(logInfo.ToJson());
                    var model = _rabbitmqChannelManagement.GetChannel(TraceLogRabbitmqConsumer.NAME);
                    model.BasicPublish("", TraceLogRabbitmqConsumer.NAME, null, buffer);

                }

            }


        }
    }
}
