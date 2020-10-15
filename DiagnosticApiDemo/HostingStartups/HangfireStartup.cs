using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
 
namespace DiagnosticApiDemo.HostingStartups
{
    public class HangfireStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //持久化
            builder.ConfigureServices((context, services) =>
            { 
                services.AddHangfire(x=>x.UseSqlServerStorage("Data Source=.;Initial Catalog=Hangfire;Integrated Security=True"));
            });


        }
    }
}
