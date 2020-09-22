using DiagnosticCore;
using Microsoft.AspNetCore.Builder;

//[assembly: PreApplicationStartMethod(typeof(DiagnosticInitialization), "InitializeDiagnostic")]
namespace Microsoft.AspNetCore.Builder
{
    public static class DiagnosticExtensions
    {
      
        public static void UseAllDiagnostic(this IApplicationBuilder app)
        {
            app.UseHttpClientDiagnostic();
            app.UseHostingDiagnostic();
        }
        public static void UseHttpClientDiagnostic(this IApplicationBuilder app)
        {
            DiagnosticInitialization.InitializeHttpClientDiagnostic(app.ApplicationServices);
        } 

        public static void UseHostingDiagnostic(this IApplicationBuilder app)
        {
            DiagnosticInitialization.InitializeHostingDiagnostic(app.ApplicationServices);
        } 

    }
}
