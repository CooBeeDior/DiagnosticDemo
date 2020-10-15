using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiagnosticCore.LogCore
{

    public class DiagnosticConstant
    {
        public static readonly EventId EVENT_ID = new EventId(123456789, "TraceInfoBuilder");

        public static string GetItemKey(string name)
        {
            return $"trace.{name?.ToLower()}";
        }


    }
}
