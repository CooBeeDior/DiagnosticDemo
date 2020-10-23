namespace SpiderCore.RequestStrategies
{
    /// <summary>
    /// 加权随机
    /// </summary>
    [RequestStrategy(StrategyType.WeightRandom)]
    public class WeightRandomRequestStrategy : WeightRequestStrategy, IRequestStrategy
    {

        public WeightRandomRequestStrategy(SpiderService spiderService) : base(spiderService)
        {

        }

        public string GetServiceIp(object param = null)
        {
            if (HealthServices.Count == 0)
            {
                throw new NotFoundServiceException(SpiderService.ServiceName);
            }
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
                        _requestStrategy = new RandomRequestStrategy(TargetSpiderService);
                    }
                }
            }
            return _requestStrategy;
     
        }





    }
}
