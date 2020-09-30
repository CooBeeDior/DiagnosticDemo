using DiagnosticModel;
using Microsoft.Extensions.Logging;
using System;

namespace DiagnosticCore.LogCore
{
    public class DiagnosticTraceLogger<T> : IDiagnosticTraceLogger<T>
    {

        private readonly ILogger<T> _logger;
        public DiagnosticTraceLogger(ILogger<T> logger)
        {
            _logger = logger;

        }
        public void Log(LogLevel logLevel, TraceInfoBuilder logbuilder, Exception exception = null)
        {
            var tranceInfo = logbuilder.Log(logLevel.ToString(), typeof(T).FullName, exception).Build();
            _logger.Log(logLevel, DiagnosticConstant.EVENT_ID, tranceInfo, exception, (loglevel,ex)=> "");
        }

        public void LogTrace(TraceInfoBuilder logbuilder, Exception exception = null)
        {
            Log(LogLevel.Trace, logbuilder, exception);
        }
        public void LogDebug(TraceInfoBuilder logbuilder, Exception exception = null)
        {
            Log(LogLevel.Debug, logbuilder, exception);
        }

        public void TraceInformation(TraceInfoBuilder logbuilder, Exception exception = null)
        {
            Log(LogLevel.Information, logbuilder, exception);
        }
        public void LogWarning(TraceInfoBuilder logbuilder, Exception exception = null)
        {
            Log(LogLevel.Warning, logbuilder, exception);
        }
        public void LogError(TraceInfoBuilder logbuilder, Exception exception = null)
        {
            Log(LogLevel.Error, logbuilder, exception);
        }
        public void LogCritical(TraceInfoBuilder logbuilder, Exception exception = null)
        {
            Log(LogLevel.Critical, logbuilder, exception);
        }


    }
}
