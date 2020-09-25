using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DiagnosticCore
{
    public class DiagnosticInitialization
    {

        public static void InitializeDiagnostic(IServiceProvider serviceProvider)
        {
            InitializeHttpClientDiagnostic(serviceProvider);
            InitializeHostingDiagnostic(serviceProvider);
        }


        public static void InitializeHttpClientDiagnostic(IServiceProvider serviceProvider)
        {
            DiagnosticListener.AllListeners.Subscribe(new HttpClientTracingDiagnosticObserver<DiagnosticListener>(listener =>
            {

                if (listener.Name == HttpClientTracingDiagnosticProcessor.ListenerName)
                {
                    //listener.Subscribe(new HttpClientTracingDiagnosticObserver<KeyValuePair<string, object>>(listenerData =>
                    //{

                    //}));
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var target = scope.ServiceProvider.GetService<IHttpClientTracingDiagnosticProcessor>() ?? new HttpClientTracingDiagnosticProcessor(serviceProvider);
                        listener.SubscribeWithAdapter(target, (a, b, c) => { return true; });
                    }
                }
            }));


        }

        public static void InitializeHostingDiagnostic(IServiceProvider serviceProvider)
        {
            DiagnosticListener.AllListeners.Subscribe(new HostingTracingDiagnosticObserver<DiagnosticListener>(listener =>
            {
                if (listener.Name == HostingTracingDiagnosticProcessor.ListenerName)
                {
                    //listener.Subscribe(new HostingTracingDiagnosticObserver<KeyValuePair<string, object>>(listenerData =>
                    //{

                    //}));
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var target = scope.ServiceProvider.GetService<IHostingTracingDiagnosticProcessor>() ?? new HostingTracingDiagnosticProcessor(serviceProvider);
                        listener.SubscribeWithAdapter(target, (a, b, c) => { return true; });
                    }
                    
                }

            }));

        }

    }
}
