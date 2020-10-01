using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SpiderCore;
namespace DiagnosticApiDemo.HostingStartups
{ 
    public class SpiderStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //持久化
            builder.ConfigureServices((context, services) =>
            {
                services.AddSpider();
            });


        }
    }
}
