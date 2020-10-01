using System;
using System.Collections.Generic;
using System.Text;

namespace SpiderCore
{
    public class SpiderHttpClientFactory : ISpiderHttpClientFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public SpiderHttpClientFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public ISpiderHttpClient CreateSpiderHttpClient(string serviceName)
        {
            return new SpiderHttpClient(serviceName, _serviceProvider);
        }
    }
}
