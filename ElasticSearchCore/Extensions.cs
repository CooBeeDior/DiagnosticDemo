using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ElasticSearchCore
{
    public static class Extensions
    {
        public static string ToIndexName(this Type @type)
        {            
            var attribute = type.GetCustomAttribute<ElasticSearchIndexAttribute>();
            if (attribute != null)
            {
                return attribute.Name;
            }
            else
            {
                return @type.FullName;
            }
        }

        public static string ToIndexName<T>(this T @type)
        {
            var attribute = typeof(T).GetCustomAttribute<ElasticSearchIndexAttribute>();
            if (attribute != null)
            {
                return attribute.Name;
            }
            else
            {
                return typeof(T).FullName;
            }
        }
    }
}
