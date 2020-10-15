using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using DiagnosticCore.Constant;

namespace DiagnosticApiDemo.HostingStartups
{

    public class DiagnosticServiceStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                //添加诊断跟踪
                services.AddDiagnostics(options =>
                {
                    options.RequestRule = (request) =>
                    {
                        if (request.Headers.Contains(HttpConstant.TRACEMICROSERVICE))
                        {
                            return true;
                        }
                        return false;
                    };
                });
                //services.AddTransient<IStartupFilter, DiagnosticStartupFilter>();
            });
        }
    }


}
