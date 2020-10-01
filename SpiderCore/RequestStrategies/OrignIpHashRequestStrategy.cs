using System;
using System.Collections.Concurrent;

namespace SpiderCore.RequestStrategies
{


    /// <summary>
    /// 源地址哈希
    /// </summary>
    public class OrignIpHashRequestStrategy : RequestStrategyBase, IRequestStrategy
    {

        ConcurrentDictionary<string, string> dictionary = new ConcurrentDictionary<string, string>();

        private IRequestStrategy _requestStrategy;
        public OrignIpHashRequestStrategy(SpiderService spiderService) : base(spiderService)
        {
            _requestStrategy = new RandomRequestStrategy(spiderService);
        }

        public string GetServiceIp(object param)
        {
            if (param == null)
            {
                throw new ArgumentNullException(nameof(param));
            }
            var ip = _requestStrategy.GetServiceIp(null);
            return dictionary.GetOrAdd(param.ToString(), ip);
        }
    }
}
