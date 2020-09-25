using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiagnosticCore
{
    public static class DiagnosticExtensions
    {
        public static void UseAllDiagnostics(this IApplicationBuilder app)
        {
            DiagnosticInitialization.InitializeDiagnostic(app.ApplicationServices);
        }

        public static void UseHttpClientDiagnostics(this IApplicationBuilder app)
        {
            DiagnosticInitialization.InitializeHttpClientDiagnostic(app.ApplicationServices);
        }


        public static void UseHostingDiagnostics(this IApplicationBuilder app)
        {
            DiagnosticInitialization.InitializeHostingDiagnostic(app.ApplicationServices);
        }
    }
}
