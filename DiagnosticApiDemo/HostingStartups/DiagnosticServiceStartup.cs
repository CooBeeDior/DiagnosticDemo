using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
namespace DiagnosticApiDemo.HostingStartups
{
     
    public class DiagnosticServiceStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                //添加诊断跟踪
                services.AddDiagnostics();
                //services.AddTransient<IStartupFilter, DiagnosticStartupFilter>();
            });
        }
    }


}
