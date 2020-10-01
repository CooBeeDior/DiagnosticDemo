using System.Threading;

namespace SpiderCore.RequestStrategies
{
    /// <summary>
    /// 轮询
    /// </summary>
    public class RoundRobinRequestStrategy : RequestStrategyBase, IRequestStrategy
    {

        private int currentIndex = 0;
        public RoundRobinRequestStrategy(SpiderService spiderService) : base(spiderService)
        {

        }
        public string GetServiceIp(object param)
        {
            int index = Interlocked.Increment(ref currentIndex);
            Interlocked.CompareExchange(ref currentIndex, 0, SpiderService.ServiceEntryies.Count);
            return SpiderService.ServiceEntryies[currentIndex].Url;
        }
    }
}
