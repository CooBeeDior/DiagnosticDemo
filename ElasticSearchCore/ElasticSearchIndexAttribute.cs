using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchCore
{
    [AttributeUsageAttribute(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ElasticSearchIndexAttribute : Attribute
    {
        public ElasticSearchIndexAttribute(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            this.Name = name;
        }
        public string Name { get; }
    }
}
