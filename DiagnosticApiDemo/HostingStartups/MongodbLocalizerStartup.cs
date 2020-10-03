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
                //本地化  需要初始化数据
                services.AddMongodbLocalizer(options =>
                {
                    options.ConnectionString = "mongodb://coobeedior.com:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false";
                    options.DatabaseName = "diagnosticapi";
                    options.CollectionName = "localizer";
                });
            });
        }
    }
}
