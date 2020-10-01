using System;
using System.Collections.Generic;
using System.Text;

namespace SpiderCore.RequestStrategies
{
    /// <summary>
    /// 加权轮询
    /// </summary>
    public class WeightRoundRobinRequestStrategy : WeightRequestStrategy, IRequestStrategy
    {


        public WeightRoundRobinRequestStrategy(SpiderService spiderService) : base(spiderService)
        {

        }

        public string GetServiceIp(object param)
        {
            var requestStrategy = GetRequestStrategy();
            return requestStrategy.GetServiceIp(null);
        }


        public override IRequestStrategy GetRequestStrategy()
        {
            return new RoundRobinRequestStrategy(SpiderService);
        }





    }
}
