using DiagnosticCore;
using DiagnosticCore.LogCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DiagnosticExtensions
    {
        public static void AddDiagnostics(this IServiceCollection services, Action<DiagnosticOptions> action = null)
        {
            DiagnosticOptions diagnosticOptions = new DiagnosticOptions();
            action?.Invoke(diagnosticOptions);
            services.AddSingleton(diagnosticOptions);
            if (diagnosticOptions.IsEnableHostingTracing)
            {
                services.AddSingleton<IHostingTracingDiagnosticProcessor, HostingTracingDiagnosticProcessor>();
                services.AddSingleton<ITracingDiagnosticProcessor, HostingTracingDiagnosticProcessor>();
            }
            if (diagnosticOptions.IsEnableHttpClientTracing)
            {
                services.AddSingleton<IHttpClientTracingDiagnosticProcessor, HttpClientTracingDiagnosticProcessor>();
                services.AddSingleton<ITracingDiagnosticProcessor, HttpClientTracingDiagnosticProcessor>();
            }
            services.AddSingleton<IObserver<DiagnosticListener>, TracingDiagnosticObserver>();
            services.AddSingleton<TracingDiagnosticObserver>(); 
            services.AddSingleton<ILoggerProvider, DiagnosticLogProvider>();
            services.Add(new ServiceDescriptor(typeof(IDiagnosticTraceLogger<>), typeof(DiagnosticTraceLogger<>), ServiceLifetime.Singleton));
 
            services.AddHttpContextAccessor();
 
        }
    }


}
