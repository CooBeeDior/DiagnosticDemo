using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
namespace DiagnosticApiDemo.HostingStartups
{
    public class MongodbLocalizerStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                //本地化
                services.AddMongodbLocalizer();
            });
        }
    }
}
