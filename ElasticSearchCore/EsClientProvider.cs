using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearchCore
{

    /// <summary>
    /// ElasticClient提供者
    /// </summary>
    public class EsClientProvider : IEsClientProvider
    {

        private ElasticSearchOptions _options;
        public EsClientProvider(ElasticSearchOptions options)
        {
            _options = options;
        }
        /// <summary>
        /// 获取elastic client
        /// </summary>
        /// <returns></returns>
        public ElasticClient GetClient()
        {
            return getClient("");
        }
        /// <summary>
        /// 指定index获取ElasticClient
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public ElasticClient GetClient(string indexName)
        {
            return getClient(indexName);
        }


        /// <summary>
        /// 根据url获取ElasticClient
        /// </summary>
        /// <param name="url"></param>
        /// <param name="defaultIndex"></param>
        /// <returns></returns>

        /// <summary>
        /// 根据urls获取ElasticClient
        /// </summary>
        /// <param name="urls"></param>
        /// <param name="defaultIndex"></param>
        /// <returns></returns>
        private ElasticClient getClient(string defaultIndex = "")
        {
            var uris = _options.Urls.Select(p => new Uri(p)).ToArray();
            var connectionPool = new StaticConnectionPool(uris);
            var connectionSetting = new ConnectionSettings(connectionPool);
            if (!string.IsNullOrWhiteSpace(defaultIndex))
            { 
                IIndexState indexState = new IndexState
                {
                    Settings = new IndexSettings
                    {
                        NumberOfReplicas = _options.NumberOfReplicas,
                        // [副本数量]
                        NumberOfShards = _options.NumberOfShards
                    }
                }; 
                connectionSetting.DefaultIndex(defaultIndex.ToLower());
          
            }
            connectionSetting.BasicAuthentication(_options.UserName, _options.Password); //设置账号密码
            return new ElasticClient(connectionSetting);
        }
    }
}
