using DiagnosticCore.Constant;
using DiagnosticCore.LogCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DiagnosticCore
{
    public static class Extensions
    {
        #region
        public static long ElapsedTime(this HttpContext context)
        {
            var stopwatch = context.Items[DiagnosticConstant.GetItemKey(HttpConstant.TRACK_TIME)] as Stopwatch;
            if (stopwatch != null)
            {
                return stopwatch.ElapsedMilliseconds;
            }
            return 0;
        }
        #endregion
    }
}
