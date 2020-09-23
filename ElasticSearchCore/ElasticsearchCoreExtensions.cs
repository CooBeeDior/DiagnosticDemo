using ElasticSearchCore;
using PersistenceAbstraction;
using System;
using System.Collections.Generic;
using System.Text;

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
            services.AddSingleton<IPersistence, ElasticSearchPersistence>();
        }
    }
}
