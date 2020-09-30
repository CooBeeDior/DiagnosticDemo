using System;
using System.Collections.Generic;
using System.Text;

namespace DiagnosticCore
{
    public interface ITracingDiagnosticProcessor
    {
        string ListenerName { get; }
    }
}
