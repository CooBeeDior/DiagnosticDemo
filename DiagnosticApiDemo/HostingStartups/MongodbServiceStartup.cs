using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
namespace DiagnosticApiDemo.HostingStartups
{

    public class LogMongodbServiceStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                //Mongodb
                services.AddLogMongodb(options =>
                {
                    options.ConnectionString = "mongodb://coobeedior.com:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false";
                    options.ConnectionName = "log";
                    options.DatabaseName = "logdb";
                });
            });


        }
    }
}
