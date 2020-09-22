using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongodbCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiagnosticCore.LogCore
{
    public class DiagnosticLogProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new DiagnosticLogger(categoryName);
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }

    public class DiagnosticLogger : ILogger
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;
        private readonly string _categoryName;
        public DiagnosticLogger(string categoryName, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            _categoryName = categoryName;
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }

        public DiagnosticLogger(string categoryName)
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
            if (_httpContextAccessor?.HttpContext != null)
            {
                _httpContextAccessor.HttpContext.ToLogInfoBuilder().BuildLog(logLevel.ToString(), typeof(TState).FullName, exception).Build().ToPersistence(_serviceProvider);
            }

        }
    }
}
