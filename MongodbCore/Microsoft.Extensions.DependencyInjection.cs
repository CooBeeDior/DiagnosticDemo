﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using MongodbCore;
using TransPortServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Microsoft.Extensions.DependencyInjection
{

    public static class MongodbExtensions
    {
        public static void AddLogMongodb(this IServiceCollection services, Action<MongoDbOptions> action = null)
        {
            MongoDbOptions options = new MongoDbOptions();
            action?.Invoke(options);

            services.AddSingleton(options);
            services.AddSingleton<IMongodbTransPortService, LogMongodbTransPortService>(); 
            TransPortServiceDependencyInjection.AddFunc((serviceProvider, name) =>
            {
                if (name.Equals(MongodbConstant.MONGODBNAME, StringComparison.OrdinalIgnoreCase))
                {
                    return serviceProvider.GetService<IMongodbTransPortService>();
                }
                return null;
            });

        }
    }











}
