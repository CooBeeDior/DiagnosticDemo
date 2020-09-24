﻿using DiagnosticCore;
using DiagnosticCore.LogCore;
using Microsoft.Extensions.Logging;
using System;

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
            services.AddSingleton<ILoggerProvider, DiagnosticLogProvider>();
            services.Add(new ServiceDescriptor(typeof(IDiagnosticTraceLogger<>), typeof(DiagnosticTraceLogger<>), ServiceLifetime.Singleton));

        }
    }


}
