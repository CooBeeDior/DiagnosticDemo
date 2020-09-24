using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceAbstraction
{

    public class PersistenceDependencyInjection
    {
        private static IList<Func<IServiceProvider, string, IPersistence>> list = new List<Func<IServiceProvider, string, IPersistence>>();


        public static void AddFunc(Func<IServiceProvider, string, IPersistence> func)
        {
            list.Add(func);
        }
        public static IList<Func<IServiceProvider, string, IPersistence>> GetFuncs()
        {
            return list;
        }

    }
}
