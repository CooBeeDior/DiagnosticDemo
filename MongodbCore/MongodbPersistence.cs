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
    public class LogMongodbPersistence : IMongodbPersistence 
    {
        private readonly IMongoCollection<BsonDocument> _mongoCollection;
        public LogMongodbPersistence(MongoDbOptions options)
        {
            //连接数据库
            var client = new MongoClient(options.ConnectionString);
            //获取database
            var mydb = client.GetDatabase(options.DatabaseName);
            //获取collection
            _mongoCollection = mydb.GetCollection<BsonDocument>(options.ConnectionName);
        }




        public async Task GetAsync<T>(T data) where T : class
        {
            var bjson = data.ToBsonDocument();
            await _mongoCollection.FindAsync(bjson);
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
