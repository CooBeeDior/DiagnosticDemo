using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Microsoft.AspNetCore.Builder;

namespace DiagnosticApiDemo.HostingStartups
{
    public class HangfireStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //持久化
            builder.ConfigureServices((context, services) =>
            {
                services.AddHangfire(x => x.UseSqlServerStorage("Data Source=.;Initial Catalog=Hangfire;Integrated Security=True"));
                services.AddSingleton<IStartupFilter, HangfireStartupFilter>();
            });


        }
    }

    public class HangfireStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseHangfireServer();//启动Hangfire服务
                app.UseHangfireDashboard();//启动hangfire面板 路由： {url}/hangfire
                next(app);
            };
        }
    }

}
