using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PersistenceAbstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MongodbCore
{
    public class MongodbPersistence : IMongodbPersistence
    {
        private readonly IMongoCollection<BsonDocument> _mongoCollection;
        public MongodbPersistence(MongoDbOptions options)
        {
            //连接数据库
            var client = new MongoClient(options.ConnectionString);
            //获取database
            var mydb = client.GetDatabase(options.ConnectionName);
            //获取collection
            _mongoCollection = mydb.GetCollection<BsonDocument>(options.ConnectionName);
        }
        public Task DeleteAsync<T>(T data) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync<T>(T data) where T : class
        {
            var bjson = data.ToBsonDocument();
            await _mongoCollection.InsertOneAsync(bjson);
        }

        public Task UpdateAsync<T>(T data) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
