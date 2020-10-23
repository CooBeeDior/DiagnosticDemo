using System;

namespace SpiderCore.RequestStrategies
{
    /// <summary>
    /// 随机
    /// </summary>
    [RequestStrategy(StrategyType.Random)]
    public class RandomRequestStrategy : RequestStrategyBase, IRequestStrategy
    {

        private readonly Random _random;
        public RandomRequestStrategy(SpiderService spiderService) : base(spiderService)
        {
            _random = new Random();
        }
        public string GetServiceIp(object param = null)
        {
            if (HealthServices.Count == 0)
            {
                throw new NotFoundServiceException(SpiderService.ServiceName);
            }
            var index = _random.Next(0, HealthServices.Count - 1);
            return HealthServices[index].Url;
        }
    }
}
