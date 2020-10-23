using System.Threading;

namespace SpiderCore.RequestStrategies
{
    /// <summary>
    /// 轮询
    /// </summary>
    [RequestStrategy(StrategyType.RoundRobin)]
    public class RoundRobinRequestStrategy : RequestStrategyBase, IRequestStrategy
    {

        private int _currentIndex;

        protected int CurrentIndex
        {
            get
            {
                if (_currentIndex >= HealthServices.Count)
                {
                    _currentIndex = 0;
                }
                return _currentIndex;
            }
            set { _currentIndex = value; }
        }
        public RoundRobinRequestStrategy(SpiderService spiderService) : base(spiderService)
        {

        }
        public string GetServiceIp(object param = null)
        {
            if (HealthServices.Count == 0)
            {
                throw new NotFoundServiceException(SpiderService.ServiceName);
            }
            string url = HealthServices[CurrentIndex].Url;
            int index = Interlocked.Increment(ref _currentIndex);
            Interlocked.CompareExchange(ref _currentIndex, 0, HealthServices.Count);
            return url;
        }
    }
}
