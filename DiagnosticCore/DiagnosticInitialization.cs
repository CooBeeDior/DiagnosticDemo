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
            DiagnosticListener.AllListeners.Subscribe(serviceProvider.GetRequiredService<IObserver<DiagnosticListener>>());
 
        }


    
    }
}
