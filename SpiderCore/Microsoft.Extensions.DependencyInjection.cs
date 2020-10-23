using Microsoft.Extensions.DependencyInjection;
using SpiderCore;
using SpiderCore.RequestStrategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SpiderCoreExtensions
    {
        public static ISpiderBuilder AddSpider(this IServiceCollection services, Action<SpiderOptions> action = null)
        {
            SpiderOptions options = new SpiderOptions();
            action?.Invoke(options);

            services.AddSingleton(options);
            services.AddHttpClient( );
            services.AddSingleton<ISpiderHttpClientFactory, SpiderHttpClientFactory>();
            services.AddSingleton<IMonitorHealthJob, MonitorHealthJob>();
            services.AddHttpContextAccessor();
           
            return new SpiderBuilder(options);
        }
    }


}
