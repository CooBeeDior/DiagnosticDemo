using Microsoft.Extensions.Logging;
using MongodbCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiagnosticCore.LogCore
{
    public class DiagnosticTraceLogger<T> : IDiagnosticTraceLogger<T>
    {

        private readonly ILogger<T> _logger;
        public DiagnosticTraceLogger(ILogger<T> logger)
        {
            _logger = logger;

        }
        public void Log(LogLevel logLevel, LogInfoBuilder logbuilder, Exception exception = null)
        {
            var loginfo = logbuilder.Log(logLevel.ToString(), typeof(T).FullName, exception).Build();
            _logger.Log(logLevel, DiagnosticConstant.EVENT_ID, loginfo, exception, (loglevel,ex)=> "");
        }

        public void LogTrace(LogInfoBuilder logbuilder, Exception exception = null)
        {
            Log(LogLevel.Trace, logbuilder, exception);
        }
        public void LogDebug(LogInfoBuilder logbuilder, Exception exception = null)
        {
            Log(LogLevel.Debug, logbuilder, exception);
        }

        public void LogInformation(LogInfoBuilder logbuilder, Exception exception = null)
        {
            Log(LogLevel.Information, logbuilder, exception);
        }
        public void LogWarning(LogInfoBuilder logbuilder, Exception exception = null)
        {
            Log(LogLevel.Warning, logbuilder, exception);
        }
        public void LogError(LogInfoBuilder logbuilder, Exception exception = null)
        {
            Log(LogLevel.Error, logbuilder, exception);
        }
        public void LogCritical(LogInfoBuilder logbuilder, Exception exception = null)
        {
            Log(LogLevel.Critical, logbuilder, exception);
        }


    }
}
