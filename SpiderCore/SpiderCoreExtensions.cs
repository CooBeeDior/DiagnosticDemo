using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpiderCore
{
    public static class SpiderCoreExtensions
    {
        public static ISpiderBuilder AddSpider(this IServiceCollection services, Action<SpiderOptions> action = null)
        {
            SpiderOptions options = new SpiderOptions();
            action?.Invoke(options);

            services.AddSingleton(options);
            services.AddHttpClient(nameof(SpiderHttpClient));
            services.AddSingleton<ISpiderHttpClientFactory, SpiderHttpClientFactory>();
            services.AddHttpContextAccessor();

            return new SpiderBuilder(options);
        }
    }


}
