using ElasticSearchCore.Models;
using Nest;
using TransPortServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchCore
{
    public class ElasticSearchTransPortService : IElasticSearchTransPortService
    {
        private readonly IEsClientProvider _esClientProvider;
        public ElasticSearchTransPortService(IEsClientProvider esClientProvider)
        {
            _esClientProvider = esClientProvider;
        }

      
        public async Task Send<T>(T data) where T : class
        {
            var indexName = typeof(T).ToIndexName();
            var client = _esClientProvider.GetClient(indexName);
            var resp = await client.CreateDocumentAsync(data); 
        }

     
    }
}
