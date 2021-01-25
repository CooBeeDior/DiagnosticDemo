using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
namespace RateLimitingCore
{
    public static class RateLimitingConfigurationExtension
    {
        public static void AddRateLimitingStrategy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRateLimiting, DefaultRateLimiting>();
            services.Configure<RateLimitingOptions>(configuration.GetSection("RateLimiting"));
        }



        public static void AddRateLimitingStrategy(this IServiceCollection services, Action<RateLimitingOptions> action)
        {
            services.AddSingleton<IRateLimiting, DefaultRateLimiting>(); 
            services.Configure<RateLimitingOptions>(action);
        }

    }
}
