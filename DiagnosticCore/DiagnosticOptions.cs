using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace DiagnosticCore
{
    public class DiagnosticOptions
    {
        public DiagnosticOptions()
        {
            RequestRule = (request) => true;
        }
        /// <summary>
        /// 是否开启httpclient追踪
        /// </summary>
        public bool IsEnableHttpClientTracing { get; set; } = true;

        /// <summary>
        /// 是否开启hsoting追踪
        /// </summary>
        public bool IsEnableHostingTracing { get; set; } = true;


        /// <summary>
        /// 请求规则 返回true则上报提交日志
        /// </summary>
        public Func<HttpRequestMessage, bool> RequestRule { get; set; }

    }



}
