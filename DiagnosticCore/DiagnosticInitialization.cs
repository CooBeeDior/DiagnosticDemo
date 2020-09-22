﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
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
                    var target = serviceProvider.GetService<IHostingTracingDiagnosticProcessor>() ?? new HostingTracingDiagnosticProcessor(serviceProvider);
                    listener.SubscribeWithAdapter(target);
                }
            }));

        }

    }
}