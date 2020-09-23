using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongodbCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using DiagnosticCore.Models;
using PersistenceAbstraction;
namespace DiagnosticCore.LogCore
{
    public class DiagnosticLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly IPersistence _persistence;

        public DiagnosticLogger(string categoryName, IServiceProvider serviceProvider)
        {
            _categoryName = categoryName;
            _persistence = serviceProvider.GetService<IPersistence>();
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
   
                    _persistence.Insert(logInfo);

                }

            }


        }
    }
}
