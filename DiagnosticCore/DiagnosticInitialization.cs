﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using MongodbCore;

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
                    var target = serviceProvider.GetService<IHttpClientTracingDiagnosticProcessor>() ?? new HttpClientTracingDiagnosticProcessor(serviceProvider);
                    listener.SubscribeWithAdapter(target);
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
 
                    //                 }));
                    var target = serviceProvider.GetService<IHostingTracingDiagnosticProcessor>() ?? new HostingTracingDiagnosticProcessor(serviceProvider);
                    listener.SubscribeWithAdapter(target);
                }

            }));

        }

    }
}
