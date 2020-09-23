using DiagnosticCore.Models;
using ElasticSearchCore;
using ElasticSearchCore.Models;
using Microsoft.AspNetCore.Builder;
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

        //public static void UseElasticsearch(this IApplicationBuilder app)
        //{
        //    var esClientProvider = app.ApplicationServices.GetService<IEsClientProvider>();
        //    esClientProvider.GetClient().CreateIndex<EsLogInfo>();
        //}
    }
}
