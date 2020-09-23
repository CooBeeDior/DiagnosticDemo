using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PersistenceAbstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongodbCore
{

    public class MongodbPersistence : IPersistence
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
        public void Delete<T>(T data) where T : class
        {
            throw new NotImplementedException();
        }

        public void Insert<T>(T data) where T : class
        {
            var bjson = data.ToBsonDocument();
            _mongoCollection.InsertOne(bjson);
        }

        public void Update<T>(T data) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
