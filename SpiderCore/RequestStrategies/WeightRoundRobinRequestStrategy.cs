using System;
using System.Collections.Generic;
using System.Text;

namespace SpiderCore.RequestStrategies
{
    /// <summary>
    /// 加权轮询
    /// </summary>
    [RequestStrategy(StrategyType.WeightRoundRobin)]
    public class WeightRoundRobinRequestStrategy : WeightRequestStrategy, IRequestStrategy
    { 

        public WeightRoundRobinRequestStrategy(SpiderService spiderService) : base(spiderService)
        {

        }

        public string GetServiceIp(object param = null)
        {
            var requestStrategy = GetRequestStrategy();
            return requestStrategy.GetServiceIp();
        }

        private IRequestStrategy _requestStrategy;
        private object obj = new object();
        public override IRequestStrategy GetRequestStrategy()
        {
            if (_requestStrategy == null)
            {
                lock (obj)
                {
                    if (_requestStrategy == null)
                    {
                        _requestStrategy = new RoundRobinRequestStrategy(TargetSpiderService);
                    }
                }
            }
            return _requestStrategy;
        }





    }
}
