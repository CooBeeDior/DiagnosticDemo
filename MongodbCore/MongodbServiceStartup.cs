using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
namespace MongodbCore
{

    public class MongodbServiceStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddMongodb(options =>
                {
                    options.ConnectionString = "mongodb://coobeedior.com:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false";
                    options.ConnectionName = "log";
                    options.DatabaseName = "logdb";
                });
            });


        }
    }
}
