using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiagnosticCore
{
    public static class DiagnosticExtensions
    {
        public static void UserAllDiagnostics(this IApplicationBuilder app)
        {
            DiagnosticInitialization.InitializeDiagnostic(app.ApplicationServices);
        }

        public static void UserHttpClientDiagnostics(this IApplicationBuilder app)
        {
            DiagnosticInitialization.InitializeHttpClientDiagnostic(app.ApplicationServices);
        }


        public static void UserHostingDiagnostics(this IApplicationBuilder app)
        {
            DiagnosticInitialization.InitializeHostingDiagnostic(app.ApplicationServices);
        }
    }
}
