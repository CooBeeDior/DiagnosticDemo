using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiagnosticCore.LogCore
{

    public class DiagnosticConstant
    {
        public static readonly EventId EVENT_ID = new EventId(100, "LogInfoBuilder");

        public static string GetItemKeyToLogBuilder(string name)
        {
            return $"{name}_Key";
        }
    }
}
