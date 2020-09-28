using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace DiagnosticApiDemo.HostingStartups
{
    public class ApiVersioningStartup : IHostingStartup
    {


        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                //版本控制
                services.AddApiVersioning(o =>
                {
                    o.ReportApiVersions = true;
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.DefaultApiVersion = new ApiVersion(1, 0);
                }).AddVersionedApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VV";//v1.0 //如果是v1.0.0--"'v'VVV"
                    options.SubstituteApiVersionInUrl = true;
                }); ;


         
            });
        }

    }


}
