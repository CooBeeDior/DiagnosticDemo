namespace SpiderCore.RequestStrategies
{
    /// <summary>
    /// 加权随机
    /// </summary>
    public class WeightRandomRequestStrategy : WeightRequestStrategy, IRequestStrategy
    {

        public WeightRandomRequestStrategy(SpiderService spiderService) : base(spiderService)
        {

        }

        public string GetServiceIp(object param)
        {
            var requestStrategy = GetRequestStrategy();
            return requestStrategy.GetServiceIp(null);
        }


        public override IRequestStrategy GetRequestStrategy()
        {
            return new RandomRequestStrategy(SpiderService);
        }





    }
}
