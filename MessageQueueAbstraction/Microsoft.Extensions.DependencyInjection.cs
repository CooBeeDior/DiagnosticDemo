using MessageQueueAbstraction;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static void AddRabbitmq(this IServiceCollection services, Action<RabbitmqOptions> action = null)
        {
            RabbitmqOptions rabbitmqOptions = new RabbitmqOptions();
            action?.Invoke(rabbitmqOptions);
            //创建连接工厂
            IConnectionFactory factory = new ConnectionFactory
            {
                UserName = rabbitmqOptions.UserName,//用户名
                Password = rabbitmqOptions.Password,//密码
                HostName = rabbitmqOptions.HostUrl,//rabbitmq ip                    
            };
            services.AddSingleton<RabbitmqOptions>(rabbitmqOptions);
            services.AddSingleton<IConnectionFactory>(factory);

            services.AddSingleton<IRabbitmqChannelManagement, RabbitmqChannelManagement>();
            //services.AddSingleton<IRabbitmqConsumer, TraceLogRabbitmqConsumer>();
            IList<Type> consumerNames = new List<Type>();
            var loadAssemblies = rabbitmqOptions.LoadAssemblies;
            if (loadAssemblies != null && loadAssemblies.Any())
            {
                foreach (var assembly in loadAssemblies)
                {
                    var consumerTypes = assembly.GetTypes().Where(type => typeof(IRabbitmqConsumer).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface && type.IsPublic).ToList();
                    if (consumerTypes != null && consumerTypes.Any())
                    {
                        foreach (var consumerType in consumerTypes)
                        {
                            if (!consumerNames.Contains(consumerType))
                            {
                                services.AddSingleton(typeof(IRabbitmqConsumer), consumerType);
                                consumerNames.Add(consumerType);
                            }

                        }
                    }
                }


            }

            var loadTypes = rabbitmqOptions.LoadTypes;
            if (loadTypes != null && loadTypes.Any())
            {
                if (loadTypes != null && loadTypes.Any())
                {
                    foreach (var consumerType in loadTypes)
                    {
                        if (!consumerNames.Contains(consumerType))
                        {
                            services.AddSingleton(typeof(IRabbitmqConsumer), consumerType);
                            consumerNames.Add(consumerType);
                        }

                    }
                }



            }
        }
        public static void UseRabbitmq(this IApplicationBuilder applicationBuilder)
        {
            var rabbitmqChannelManagement = applicationBuilder.ApplicationServices.GetService<IRabbitmqChannelManagement>();
            var rabbitmqConsumers = applicationBuilder.ApplicationServices.GetServices<IRabbitmqConsumer>();
            if (rabbitmqConsumers != null && rabbitmqConsumers.Any())
            {
                foreach (var item in rabbitmqConsumers)
                {
                    rabbitmqChannelManagement.Declare(item);
                    item.Subscripe();
                }
            }

        }



    }
























    }
