﻿using FreeSql;
using FreeSql.Internal;
using Microsoft.Extensions.DependencyInjection;
using PersistenceAbstraction;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace FreesqlAbstration
{

    public static class FreeSqlExtensions
    {
        public static void AddFreeSql(this IServiceCollection services, Action<FreeSqlOptions> action = null)
        {
            FreeSqlOptions freeSqlOptions = new FreeSqlOptions();
            action?.Invoke(freeSqlOptions);
            services.AddSingleton(freeSqlOptions);
            IdleBus<IFreeSql> ib = new IdleBus<IFreeSql>(freeSqlOptions.TimeSpan);
            Dictionary<string, IFreeSql> dic = new Dictionary<string, IFreeSql>();
            foreach (var item in freeSqlOptions.FreeSqlDbs)
            {
                var freesql = new FreeSqlBuilder()
                .UseConnectionString(item.DataType, item.ConnectString)
                .UseAutoSyncStructure(item.IsAutoSyncStructure) //自动同步实体结构到数据库
                .UseNoneCommandParameter(item.IsNoneCommandParameter)
                .UseLazyLoading(item.IsLazyLoading)
                .UseNameConvert(item.NameConvertType)
                .UseMonitorCommand(item.Executing, item.Executed).Build();
                dic.Add(item.Name, freesql);
                var isRegisterSucess = ib.TryRegister(item.Name, () =>
                     {
                         return freesql;
                     });
                if (!isRegisterSucess)
                {
                    throw new Exception($"{item.Name}数据库注入失败");
                }

            }
            services.AddSingleton<Func<string, IFreeSql>>(name =>
            {
                if (dic.ContainsKey(name))
                {
                    return dic[name];
                }
                throw new Exception($"not find freesql by name :{name}");
            });
            services.AddSingleton<IFreeSqlPersistence, FreeSqlPersistence>();
            services.AddSingleton(ib);
            PersistenceDependencyInjection.AddFunc((serviceProvider, name) =>
            {
                if (name.Equals("FreeSql", StringComparison.OrdinalIgnoreCase))
                {
                    return serviceProvider.GetService<IFreeSqlPersistence>();
                }
                return null;
            });
        }
    }
}
