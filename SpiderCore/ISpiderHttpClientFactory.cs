using System;

namespace SpiderCore
{
    public interface ISpiderHttpClientFactory
    {
        ISpiderHttpClient CreateSpiderHttpClient(string serviceName);
    }
}
