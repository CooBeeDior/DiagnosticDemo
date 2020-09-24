using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using MongodbCore;
using PersistenceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Microsoft.Extensions.DependencyInjection
{

    public static class MongodbExtensions
    {
        public static void AddMongodb(this IServiceCollection services, Action<MongoDbOptions> action = null)
        {
            MongoDbOptions options = new MongoDbOptions();
            action?.Invoke(options);


            services.AddSingleton(options);
            services.AddSingleton<IMongodbPersistence, MongodbPersistence>();
            PersistenceDependencyInjection.AddFunc((serviceProvider, name) =>
            {
                if (name.Equals("Mongodb", StringComparison.OrdinalIgnoreCase))
                {
                    return serviceProvider.GetService<IMongodbPersistence>();
                }
                return null;
            });

        }
    }











}
