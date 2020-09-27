using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace DiagnosticApiDemo.HostingStartups
{
    public class CorsStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                //services.AddCors(options => options.AddPolicy("CorsPolicy",
                // builder =>
                // {
                //     builder.AllowAnyMethod()
                //         .AllowAnyHeader()
                //         .AllowAnyOrigin()
                //         .AllowCredentials();
                // }));// 注册CORS服务

            });


        }
    }
}
