using DiagnosticModel;
using Microsoft.Extensions.Logging;
using System;
namespace DiagnosticCore.LogCore
{
    public class DiagnosticLogger : ILogger
    {
        private readonly string _categoryName;
   

        public DiagnosticLogger(string categoryName, IServiceProvider serviceProvider)
        {
            _categoryName = categoryName; 
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
           

                }

            }


        }
    }
}
