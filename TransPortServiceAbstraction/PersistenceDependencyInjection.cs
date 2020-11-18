using System;
using System.Collections.Generic;
using System.Text;

namespace TransPortServiceAbstraction
{

    public class TransPortServiceDependencyInjection
    {
        private static IList<Func<IServiceProvider, string, ITransPortService>> list = new List<Func<IServiceProvider, string, ITransPortService>>();


        public static void AddFunc(Func<IServiceProvider, string, ITransPortService> func)
        {
            list.Add(func);
        }
        public static IList<Func<IServiceProvider, string, ITransPortService>> GetFuncs()
        {
            return list;
        }

    }
}
