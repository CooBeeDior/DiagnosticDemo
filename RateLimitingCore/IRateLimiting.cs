using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RateLimitingCore
{
    /// <summary>
    /// 限频策略
    /// </summary>
    public interface IRateLimiting
    {
        public Task<bool> IsLimitAsync(HttpRequestMessage request);
    }
}
