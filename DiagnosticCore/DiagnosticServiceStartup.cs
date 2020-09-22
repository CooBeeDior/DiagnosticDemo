using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
namespace DiagnosticCore
{
     
    public class DiagnosticServiceStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDiagnostics();
                //services.AddTransient<IStartupFilter, DiagnosticStartupFilter>();
            });
        }
    }


}
