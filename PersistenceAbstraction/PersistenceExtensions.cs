using Microsoft.Extensions.DependencyInjection;
using PersistenceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PersistenceExtensions
    {
        public static void AddPersistence(this IServiceCollection services)
        {
            Func<IServiceProvider, Func<string, IPersistence>> func = serviceProvider => (name) =>
            {
                if (PersistenceDependencyInjection.GetFuncs().Any())
                {
                    foreach (var item in PersistenceDependencyInjection.GetFuncs())
                    {
                        try
                        {
                            var persistence = item.Invoke(serviceProvider, name);
                            if (persistence != null)
                            {
                                return persistence;
                            }
                        }
                        catch
                        {

                        }
                    } 
                }
                throw new Exception($"not found service by name {name}");
            };


            services.AddSingleton<Func<string, IPersistence>>(func);
        }
    }
}
