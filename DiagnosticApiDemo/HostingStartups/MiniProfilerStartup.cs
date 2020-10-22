using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Profiling;
using Microsoft.AspNetCore.Builder;

namespace DiagnosticApiDemo.HostingStartups
{
    public class MiniProfilerStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddMiniProfiler(options => {
                    // 设定弹出窗口的位置
                    options.PopupRenderPosition = RenderPosition.Left;
                    // 设定访问分析结果URL的路由基地址
                    options.RouteBasePath = "/profiler";
                 
                });//.AddEntityFramework(); ;
                services.AddSingleton<IStartupFilter, MiniProfilerStartupFilter>();
            });


        }

        public class MiniProfilerStartupFilter : IStartupFilter
        {
            public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
            {
                return app =>
                {
                    //路由：  /profiler/results
                    app.UseMiniProfiler();
                    next(app);
                };
            }
        }
    }
}
