using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using MessageQueueAbstraction;
using System.Reflection;

namespace DiagnosticApiDemo.HostingStartups
{
    public class RabbitmqServiceStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                //消息队列
                services.AddRabbitmq(options =>
                {
                    options.HostUrl = "47.111.87.132";
                    options.UserName = "admin";
                    options.Password = "123456";
                    options.LoadTypes.Add(typeof(TraceLogRabbitmqConsumer));
                    options.LoadAssemblies.Add(typeof(TraceLogRabbitmqConsumer).Assembly);
                });
            });
        }
    }
}
