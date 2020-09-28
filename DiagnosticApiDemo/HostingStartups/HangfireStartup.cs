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
                services.AddHangfire(x=>x.UseSqlServerStorage("Data Source=47.111.87.132;Initial Catalog=hangfire;User ID=dev;Password=Zhouqazwsx123"));
            });


        }
    }
}
