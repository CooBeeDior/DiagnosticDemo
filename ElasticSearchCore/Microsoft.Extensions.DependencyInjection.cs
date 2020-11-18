using ElasticSearchCore;
using TransPortServiceAbstraction;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ElasticsearchCoreExtensions
    {
        public static void AddElasticsearch(this IServiceCollection services, Action<ElasticSearchOptions> action = null)
        {
            ElasticSearchOptions options = new ElasticSearchOptions();
            action?.Invoke(options);

            services.AddSingleton(options);
            services.AddSingleton<IEsClientProvider, EsClientProvider>(); 
            services.AddSingleton<IElasticSearchTransPortService, ElasticSearchTransPortService>();

            
            TransPortServiceDependencyInjection.AddFunc((serviceProvider, name) =>
            {
                if (name.Equals(ElasticsearchConstant.ELASTICSEARCHNAME, StringComparison.OrdinalIgnoreCase))
                {
                    return serviceProvider.GetService<IElasticSearchTransPortService>();
                }
                return null;
            });
        }


    }
}
