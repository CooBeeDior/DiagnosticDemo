using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using DiagnosticCore.Constant;
using System.Text.RegularExpressions;
using SpiderCore;
using DiagnosticCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace DiagnosticApiDemo.HostingStartups
{

    public class DiagnosticServiceStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                //添加诊断跟踪
                services.AddDiagnostics();
                services.AddSingleton<IStartupFilter, DiagnosticStartupFilter>();
            });
        }
    }


    public class DiagnosticStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                //添加诊断
                app.UseDiagnostics();
                next(app);
            };
        }
    }




}
