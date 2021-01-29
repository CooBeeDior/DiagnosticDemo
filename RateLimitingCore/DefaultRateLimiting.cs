using DiagnosticModel;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RateLimitingCore
{
    //public class DefaultRateLimiting : IRateLimiting
    //{
    //    private readonly ILogger<DefaultRateLimiting> _logger;
    //    private readonly IOptionsMonitor<RateLimitingOptions> _rateLimitingOptions;
    //    private static readonly ConcurrentDictionary<string, RateLimitCounter> data = new ConcurrentDictionary<string, RateLimitCounter>();


    //    public DefaultRateLimiting(ILogger<DefaultRateLimiting> logger,
    //        IOptionsMonitor<RateLimitingOptions> rateLimitingOptions)
    //    {
    //        _logger = logger;
    //        _rateLimitingOptions = rateLimitingOptions;
    //    }


    //    public async Task<bool> IsLimitAsync(HttpRequestMessage request)
    //    {
    //        foreach (var rateLimiting in _rateLimitingOptions.CurrentValue.List.Where(o => o.EnableRateLimiting))
    //        {
    //            foreach (var url in rateLimiting.Urls)
    //            {
    //                if (request.RequestUri.ToString().Contains(url, StringComparison.InvariantCultureIgnoreCase))
    //                {
    //                    int retry = 0;
    //                    do
    //                    {
    //                        string key = $"{_rateLimitingOptions.CurrentValue.Name}_{IpExtension.GetIp()}_{url.ToLower()}";


    //                        var entry = data.GetOrAdd(key, (key) =>
    //                        {
    //                            var counter = new RateLimitCounter
    //                            {
    //                                Timestamp = DateTime.UtcNow,
    //                                Count = 1
    //                            };
    //                            return counter;
    //                        });

    //                        // entry has not expired
    //                        if (entry.Timestamp + TimeSpan.FromSeconds(rateLimiting.Period) >= DateTime.UtcNow)
    //                        {
    //                            // increment request count
    //                            var totalCount = entry.Count + 1;                                 
    //                            entry.Count = totalCount;
    //                        }

    //                        _logger.LogInformation($"问卷检测请求频率：{url.ToLower()}----{entry.ToJson()}");

    //                        if (entry.Count <= rateLimiting.Limit)
    //                        {
    //                            return true;
    //                        }
    //                        retry++;

    //                        TimeSpan afterTimeSpan = entry.Timestamp + TimeSpan.FromSeconds(rateLimiting.Period) - DateTime.UtcNow;
    //                        if (afterTimeSpan.TotalSeconds > 0)
    //                        {
    //                            await Task.Delay((int)(afterTimeSpan.TotalSeconds * 1000));
    //                        }
    //                    } while (retry <= rateLimiting.Retry);
    //                    return false;

    //                }
    //            }
    //        }
    //        return true;
    //    }
    //}


    public class DefaultRateLimiting : IRateLimiting
    {
        private readonly ILogger<DefaultRateLimiting> _logger;
        private readonly IOptionsMonitor<RateLimitingOptions> _rateLimitingOptions;
        private readonly IMemoryCache _memoryCache;


        public DefaultRateLimiting(ILogger<DefaultRateLimiting> logger,
            IOptionsMonitor<RateLimitingOptions> rateLimitingOptions,
             IMemoryCache memoryCache)
        {
            _logger = logger;
            _rateLimitingOptions = rateLimitingOptions;
            _memoryCache = memoryCache;
        }


        public async Task<bool> IsLimitAsync(HttpRequestMessage request)
        {
            foreach (var rateLimiting in _rateLimitingOptions.CurrentValue.List.Where(o => o.EnableRateLimiting))
            {
                foreach (var url in rateLimiting.Urls)
                {
                    if (request.RequestUri.ToString().Contains(url, StringComparison.InvariantCultureIgnoreCase))
                    {
                        int retry = 0;
                        do
                        {
                            string key = $"byt.srv.survey:wenjuan:{IpExtension.GetIp()}_{url.ToLower()}";
                            var counter = new RateLimitCounter
                            {
                                Timestamp = DateTime.UtcNow,
                                Count = 1
                            };

                            var entry = _memoryCache.Get<RateLimitCounter>(key);
                            if (entry.Count > 0)
                            {
                                // entry has not expired
                                if (entry.Timestamp + TimeSpan.FromSeconds(rateLimiting.Period) >= DateTime.UtcNow)
                                {
                                    // increment request count
                                    var totalCount = entry.Count + 1;

                                    // deep copy
                                    counter = new RateLimitCounter
                                    {
                                        Timestamp = entry.Timestamp,
                                        Count = totalCount
                                    };
                                }
                            }
                            _logger.LogInformation($"问卷检测请求频率：{url.ToLower()}----{counter.ToJson()}");
                            _memoryCache.Set(key, counter, TimeSpan.FromSeconds(rateLimiting.Period));
                            if (counter.Count <= rateLimiting.Limit)
                            {
                                return true;
                            }
                            retry++;

                            TimeSpan afterTimeSpan = counter.Timestamp + TimeSpan.FromSeconds(rateLimiting.Period) - DateTime.UtcNow;
                            if (afterTimeSpan.TotalSeconds > 0)
                            {
                                await Task.Delay((int)(afterTimeSpan.TotalSeconds * 1000));
                            }
                        } while (retry <= rateLimiting.Retry);
                        return false;

                    }
                }
            }
            return true;
        }

    }
}
