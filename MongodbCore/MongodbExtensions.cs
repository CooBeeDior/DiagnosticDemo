using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using MongodbCore;
using PersistenceAbstraction;
using System;

namespace Microsoft.Extensions.DependencyInjection
{

    public static class MongodbExtensions
    {
        public static void AddMongodb(this IServiceCollection services, Action<MongoDbOptions> action = null)
        {
            MongoDbOptions options = new MongoDbOptions();
            action?.Invoke(options);
    
 
            services.ConfigureOptions(options);
            services.AddSingleton<IPersistence,MongodbPersistence>();

        }
    }











}
