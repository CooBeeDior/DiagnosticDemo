using Microsoft.Extensions.DependencyInjection;
using TransPortServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TransPortServiceExtensions
    {
        public static void AddTransPortService(this IServiceCollection services)
        {
            Func<IServiceProvider, Func<string, ITransPortService>> func = serviceProvider => (name) =>
            {
                if (TransPortServiceDependencyInjection.GetFuncs().Any())
                {
                    foreach (var item in TransPortServiceDependencyInjection.GetFuncs())
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


            services.AddSingleton<Func<string, ITransPortService>>(func);
        }
    }
}
