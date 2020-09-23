using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongodbCore
{
    public interface IPersistence
    {
        void Insert<T>(T data);

        void Update<T>(T data);

        void Delete<T>(T data);
    }


    public class MongodbPersistence : IPersistence
    {
        private readonly IMongoCollection<BsonDocument> _mongoCollection;
        public MongodbPersistence(IMongoCollection<BsonDocument> mongoCollection)
        {
            _mongoCollection = mongoCollection;
        }
        public void Delete<T>(T data)
        {
            throw new NotImplementedException();
        }

        public void Insert<T>(T data)
        {
            var bjson = data.ToBsonDocument();
            _mongoCollection.InsertOne(bjson);
        }

        public void Update<T>(T data)
        {
            throw new NotImplementedException();
        }
    }
}
