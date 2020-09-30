using ElasticSearchCore;
using PersistenceAbstraction;
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
            services.AddSingleton<IElasticSearchPersistence, ElasticSearchPersistence>();

            
            PersistenceDependencyInjection.AddFunc((serviceProvider, name) =>
            {
                if (name.Equals(ElasticsearchConstant.ELASTICSEARCHNAME, StringComparison.OrdinalIgnoreCase))
                {
                    return serviceProvider.GetService<IElasticSearchPersistence>();
                }
                return null;
            });
        }


    }
}
