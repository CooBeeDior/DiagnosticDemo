using System;
using System.Collections.Generic;
using System.Text;

namespace DiagnosticCore
{
    public class DiagnosticOptions
    {
        /// <summary>
        /// 是否开启httpclient追踪
        /// </summary>
        public bool IsEnableHttpClientTracing { get; set; } = true;

        /// <summary>
        /// 是否开启hsoting追踪
        /// </summary>
        public bool IsEnableHostingTracing { get; set; } = true;


    }
}
