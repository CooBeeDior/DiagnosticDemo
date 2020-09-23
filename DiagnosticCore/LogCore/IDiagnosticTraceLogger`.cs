using Microsoft.Extensions.Logging;
using MongodbCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiagnosticCore.LogCore
{
    public interface IDiagnosticTraceLogger<T>
    {
        void Log(LogLevel logLevel, LogInfoBuilder logbuilder, Exception exception = null);


        void LogTrace(LogInfoBuilder logbuilder, Exception exception = null);

        void LogDebug(LogInfoBuilder logbuilder, Exception exception = null);

        void LogInformation(LogInfoBuilder logbuilder, Exception exception=null);

        void LogWarning(LogInfoBuilder logbuilder, Exception exception = null);

        void LogError(LogInfoBuilder logbuilder, Exception exception = null);

        void LogCritical(LogInfoBuilder logbuilder, Exception exception = null);

    }
}
