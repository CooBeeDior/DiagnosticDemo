using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MessageQueueAbstraction
{
    public class RabbitmqOptions
    {
        public RabbitmqOptions()
        {
            LoadAssemblies = new List<Assembly>();
            LoadTypes = new List<Type>();
        }
        public string UserName { get; set; }

        public string Password { get; set; }

        public string HostUrl { get; set; }

        public IList<Assembly> LoadAssemblies { get; set; }

        public IList<Type> LoadTypes { get; set; }

    }
}
