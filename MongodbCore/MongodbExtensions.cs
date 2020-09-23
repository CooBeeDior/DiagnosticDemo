using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using MongodbCore;
using System;

namespace Microsoft.Extensions.DependencyInjection
{

    public static class MongodbExtensions
    {
        public static void AddMongodb(this IServiceCollection services, Action<MongoDbOptions> action = null)
        {
            MongoDbOptions options = new MongoDbOptions();

            action?.Invoke(options);
            //连接数据库
            var client = new MongoClient(options.ConnectionString);
            //获取database
            var mydb = client.GetDatabase(options.ConnectionName);
            //获取collection
            var collection = mydb.GetCollection<BsonDocument>(options.ConnectionName);

            services.AddSingleton<IMongoClient>(client);
            services.AddSingleton<IMongoDatabase>(mydb);
            services.AddSingleton<IMongoCollection<BsonDocument>>(collection);
            services.AddSingleton<IPersistence,MongodbPersistence>();

        }
    }











}
