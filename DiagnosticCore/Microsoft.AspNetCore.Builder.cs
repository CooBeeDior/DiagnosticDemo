using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DiagnosticCore
{
    public static class DiagnosticExtensions
    {
        public static void UseDiagnostics(this IApplicationBuilder app)
        {
            DiagnosticInitialization.InitializeDiagnostic(app.ApplicationServices); 
        }

   
    }
}
