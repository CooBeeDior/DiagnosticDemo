using DiagnosticCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DiagnosticExtensions
    {
        public static void AddDiagnostics(this IServiceCollection services, Action<DiagnosticOptions> options = null)
        {
            DiagnosticOptions diagnosticOptions = new DiagnosticOptions();
            options?.Invoke(diagnosticOptions);
            if (diagnosticOptions.IsEnableHostingTracing)
            {
                services.AddSingleton<IHostingTracingDiagnosticProcessor, HostingTracingDiagnosticProcessor>();
            }
            if (diagnosticOptions.IsEnableHttpClientTracing)
            {
                services.AddSingleton<IHttpClientTracingDiagnosticProcessor, HttpClientTracingDiagnosticProcessor>();
            }
             
        }
    }

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
