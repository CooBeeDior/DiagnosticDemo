using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace DiagnosticApiDemo.HostingStartups
{
    public class ElasticSearchServiceStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                //Elasticsearch
                services.AddElasticsearch(options =>
                {
                    options.Urls = new List<string>() { "http://47.111.87.132:9200/" };
                });
            });


        }
    }
}
