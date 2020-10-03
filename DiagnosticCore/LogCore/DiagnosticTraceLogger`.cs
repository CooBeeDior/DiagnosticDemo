using DiagnosticModel;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

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
            var traceInfo = logbuilder.Build();
            if (string.IsNullOrEmpty(traceInfo.LogLevel))
            {
                traceInfo.LogLevel = logLevel.ToString();
            }
            if (string.IsNullOrEmpty(traceInfo.LogName))
            {
                traceInfo.LogName = typeof(T).FullName;
            }
            if (traceInfo.Exception == null)
            {
                traceInfo.Exception = exception;
            }
            if (string.IsNullOrEmpty(traceInfo.ErrorMessage))
            {
                traceInfo.ErrorMessage = exception?.Message;
            }
            if (string.IsNullOrEmpty(traceInfo.Description))
            {
                traceInfo.Description = $"请求url:{traceInfo?.Request?.Url}";
            }
            if (string.IsNullOrEmpty(traceInfo.ServerName))
            {
                traceInfo.ServerName = AppDomain.CurrentDomain.FriendlyName;
            }
            _logger.Log(logLevel, DiagnosticConstant.EVENT_ID, traceInfo, exception, (loglevel, ex) => "");
        }

        public void LogTrace(TraceInfoBuilder logbuilder, Exception exception = null)
        {
            Log(LogLevel.Trace, logbuilder, exception);
        }
        public void LogDebug(TraceInfoBuilder logbuilder, Exception exception = null)
        {
            Log(LogLevel.Debug, logbuilder, exception);
        }

        public void LogInformation(TraceInfoBuilder logbuilder, Exception exception = null)
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
