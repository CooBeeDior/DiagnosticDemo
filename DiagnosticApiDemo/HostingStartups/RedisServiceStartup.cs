using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed;

namespace DiagnosticApiDemo.HostingStartups
{
    

    public class RedisServiceStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //redis缓存 IDistributedCache
            builder.ConfigureServices((context, services) =>
            {
                services.AddDistributedRedisCache(opts =>
                {
                    opts.Configuration = "47.111.87.132:6379,password=Zhou123456,connectTimeout=1000";
                    opts.InstanceName = "diagnostic";

                });
            });
        }
    }
}
