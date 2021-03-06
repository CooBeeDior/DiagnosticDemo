﻿using FreeSql;
using FreesqlAbstration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TransPortServiceAbstraction;

namespace Microsoft.Extensions.DependencyInjection
{

    public static class FreeSqlExtensions
    {
        public static void AddFreeSql(this IServiceCollection services, Action<FreeSqlOptions> action = null)
        {
            FreeSqlOptions freeSqlOptions = new FreeSqlOptions();
            action?.Invoke(freeSqlOptions);
            services.AddSingleton(freeSqlOptions);
            IdleBus<IFreeSql> ib = new IdleBus<IFreeSql>(freeSqlOptions.TimeSpan);
            //Dictionary<string, IFreeSql> dic = new Dictionary<string, IFreeSql>();
            foreach (var item in freeSqlOptions.FreeSqlDbs)
            { 
                var isRegisterSucess = ib.TryRegister(item.Name, () =>
                {
                    var freesql = new FreeSqlBuilder()
                     .UseConnectionString(item.DataType, item.ConnectString)
                     .UseAutoSyncStructure(item.IsAutoSyncStructure) //自动同步实体结构到数据库
                     .UseNoneCommandParameter(item.IsNoneCommandParameter)
                     .UseLazyLoading(item.IsLazyLoading)
                     .UseNameConvert(item.NameConvertType)
                     .UseMonitorCommand(item.Executing, item.Executed).Build();
                    freesql.UseJsonMap();
                    return freesql;
                     });
                if (!isRegisterSucess)
                {
                    throw new Exception($"{item.Name}数据库注入失败");
                }

            }
           
            services.AddSingleton<IFreeSqlTransPortService, FreeSqlTransPortService>();
            services.AddSingleton(ib);
            TransPortServiceDependencyInjection.AddFunc((serviceProvider, name) =>
            {
                if (name.Equals(FreeSqlConstant.FREESQLNAME, StringComparison.OrdinalIgnoreCase))
                {
                    return serviceProvider.GetService<IFreeSqlTransPortService>();
                }
                return null;
            });
        }
    }
}
