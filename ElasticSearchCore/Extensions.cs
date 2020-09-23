using Nest;
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
                return attribute.Name.ToLower();
            }
            else
            {
                return @type.FullName.ToLower();
            }
        }

        public static string ToIndexName<T>(this T @type)
        {
            var attribute = typeof(T).GetCustomAttribute<ElasticSearchIndexAttribute>();
            if (attribute != null)
            {
                return attribute.Name.ToLower();
            }
            else
            {
                return typeof(T).FullName.ToLower();
            }
        }

        public static bool CreateIndex<T>(this ElasticClient client) where T : class
        {
            string indexName = typeof(T).ToIndexName();
            if (!client.Indices.Exists(indexName).Exists)
            {
                var response = client.Indices.Create(indexName, p => p.Map<T>(p => p.AutoMap()));
                return response.Acknowledged;
            }
            return false;
            

        }
    }
}
