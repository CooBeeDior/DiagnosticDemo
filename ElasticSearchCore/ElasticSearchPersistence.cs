using ElasticSearchCore.Models;
using Nest;
using PersistenceAbstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchCore
{ 
    public class ElasticSearchPersistence : IPersistence
    {
        private readonly IEsClientProvider _esClientProvider;
        public ElasticSearchPersistence(IEsClientProvider esClientProvider)
        {
            _esClientProvider = esClientProvider;
        }

        public void Delete<T>(T data) where T : class
        {
            throw new NotImplementedException();
        }

        public void Insert<T>(T data) where T : class
        {
            var indexName = typeof(T).ToIndexName();
            var client = _esClientProvider.GetClient(indexName);
            var resp = client.CreateDocument(data);


        }

        public void Update<T>(T data) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
