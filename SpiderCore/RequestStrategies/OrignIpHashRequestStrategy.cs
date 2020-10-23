using System;
using System.Collections.Concurrent;
using System.Linq;

namespace SpiderCore.RequestStrategies
{
    /// <summary>
    /// 源地址哈希
    /// </summary>
    [RequestStrategy(StrategyType.OrignIpHash)]
    public class OrignIpHashRequestStrategy : RequestStrategyBase, IRequestStrategy
    {
        private IRequestStrategy _requestStrategy;
        private object obj = new object();
        ConcurrentDictionary<string, string> dictionary = new ConcurrentDictionary<string, string>();

        public OrignIpHashRequestStrategy(SpiderService spiderService) : base(spiderService)
        {
            if (_requestStrategy == null)
            {
                lock (obj)
                {
                    if (_requestStrategy == null)
                    {
                        _requestStrategy = new RoundRobinRequestStrategy(SpiderService);
                    }
                }
            }
        }

        public string GetServiceIp(object param = null)
        {
            if (param == null)
            {
                throw new ArgumentNullException(nameof(param));
            }
            if (HealthServices.Count == 0)
            {
                throw new NotFoundServiceException(SpiderService.ServiceName);
            }
            var ip = _requestStrategy.GetServiceIp();

            var targetIp = dictionary.GetOrAdd(param.ToString(), ip);
            if (!HealthServices.Any(o => o.Url == targetIp))
            {
                targetIp = GetServiceIp(param);
            }
            return targetIp;
        }
    }
}
