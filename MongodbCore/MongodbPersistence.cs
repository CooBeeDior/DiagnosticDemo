using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TransPortServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MongodbCore
{
    public class LogMongodbTransPortService : IMongodbTransPortService
    {
        private readonly IMongoCollection<BsonDocument> _mongoCollection;
        public LogMongodbTransPortService(MongoDbOptions options)
        {
            //连接数据库
            var client = new MongoClient(options.ConnectionString);
            //获取database
            var mydb = client.GetDatabase(options.DatabaseName);
            //获取collection
            _mongoCollection = mydb.GetCollection<BsonDocument>(options.CollectionName);
        }
 
        public async Task Send<T>(T data) where T : class
        {
            var bjson = data.ToBsonDocument();
            await _mongoCollection.InsertOneAsync(bjson);
        }

        
    }
}
