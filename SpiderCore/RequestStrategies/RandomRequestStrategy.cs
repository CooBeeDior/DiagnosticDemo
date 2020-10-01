using System;

namespace SpiderCore.RequestStrategies
{
    /// <summary>
    /// 随机
    /// </summary>
    public class RandomRequestStrategy : RequestStrategyBase, IRequestStrategy
    {

        private readonly Random _random;
        public RandomRequestStrategy(SpiderService spiderService) : base(spiderService)
        {
            _random = new Random();
        }
        public string GetServiceIp(object param)
        {
            var index = _random.Next(0, SpiderService.ServiceEntryies.Count - 1);
            return SpiderService.ServiceEntryies[index].Url;
        }
    }
}
