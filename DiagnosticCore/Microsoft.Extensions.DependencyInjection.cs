using DiagnosticCore;
using DiagnosticCore.Constant;
using DiagnosticCore.LogCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

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


            //RequestRuleRegister.Register((serviceProvider, request) =>
            //{
            //    if (!(request.Headers.ContainsKey(HttpConstant.TRACEMICROSERVICE) && Regex.Match(request.Path, @"^(/v\d\.\d)?/api").Success))
            //    {
            //        return false;
            //    }
            //    return true;
            //});
            diagnosticOptions.SetRequestRequestRule((serviceProvider, request) =>
            {
                if (serviceProvider == null || request == null)
                {
                    return false;
                }
                foreach (var rule in RequestRuleRegister.GetAll())
                {
                    bool flag = rule.Invoke(serviceProvider, request);
                    if (!flag)
                    {
                        return flag;
                    }
                }
                return true;
            });
            services.AddHttpContextAccessor();

        }
    }

    public static class RequestRuleRegister
    {
        private static IList<Func<IServiceProvider, HttpRequest, bool>> rules = new List<Func<IServiceProvider, HttpRequest, bool>>();
        public static void Register(Func<IServiceProvider, HttpRequest, bool> requestRule)
        {
            rules.Add(requestRule);
        }
        public static IList<Func<IServiceProvider, HttpRequest, bool>> GetAll()
        {
            return rules;
        }

    }


}
