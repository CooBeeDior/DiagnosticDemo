using ElasticSearchCore.Models;
using Nest;
using PersistenceAbstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchCore
{
    public class ElasticSearchPersistence : IElasticSearchPersistence
    {
        private readonly IEsClientProvider _esClientProvider;
        public ElasticSearchPersistence(IEsClientProvider esClientProvider)
        {
            _esClientProvider = esClientProvider;
        }

        public Task DeleteAsync<T>(T data) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync<T>(T data) where T : class
        {
            var indexName = typeof(T).ToIndexName();
            var client = _esClientProvider.GetClient(indexName);
            var resp = await client.CreateDocumentAsync(data); 
        }

        public Task UpdateAsync<T>(T data) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
