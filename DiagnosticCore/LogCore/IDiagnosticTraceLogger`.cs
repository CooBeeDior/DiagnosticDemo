using DiagnosticModel;
using Microsoft.Extensions.Logging;
using System;

namespace DiagnosticCore.LogCore
{
    public interface IDiagnosticTraceLogger<T>
    {
        void Log(LogLevel logLevel, TraceInfoBuilder logbuilder, Exception exception = null);


        void LogTrace(TraceInfoBuilder logbuilder, Exception exception = null);

        void LogDebug(TraceInfoBuilder logbuilder, Exception exception = null);

        void LogInformation(TraceInfoBuilder logbuilder, Exception exception=null);

        void LogWarning(TraceInfoBuilder logbuilder, Exception exception = null);

        void LogError(TraceInfoBuilder logbuilder, Exception exception = null);

        void LogCritical(TraceInfoBuilder logbuilder, Exception exception = null);

    }
}
