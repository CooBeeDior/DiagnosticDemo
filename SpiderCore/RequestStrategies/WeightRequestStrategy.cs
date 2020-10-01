using System.Linq;

namespace SpiderCore.RequestStrategies
{

    /// <summary>
    /// 加权抽象
    /// </summary>
    public abstract class WeightRequestStrategy : RequestStrategyBase
    {
        private IRequestStrategy _requestStrategy;
        public WeightRequestStrategy(SpiderService spiderService) : base(spiderService)
        {
            SpiderService targetSpiderService = new SpiderService()
            {
                ServiceName = spiderService.ServiceName,
                ArithmeticType = spiderService.ArithmeticType,
                StrategyType = spiderService.StrategyType

            };

            var isWeightServices = spiderService.ServiceEntryies.Where(o => o.Weight > 0);
            var weights = isWeightServices.Select(o => o.Weight).ToArray();
            int maxFactor = maxCommonFactor(weights);

            foreach (var item in isWeightServices)
            {
                int num = item.Weight / maxFactor;
                for (int i = 0; i < num; i++)
                {
                    SpiderServiceEntry spiderServiceEntry = new SpiderServiceEntry()
                    {
                        Url = item.Url,
                        Weight = item.Weight,
                        IsHealth = item.IsHealth
                    };
                    targetSpiderService.ServiceEntryies.Add(spiderServiceEntry);
                }
            }

            SpiderService = targetSpiderService;
        }

        public abstract IRequestStrategy GetRequestStrategy();

        private int maxCommonFactor(params int[] parameters)
        {
            int i;
            int c = parameters[0];
            for (i = 1; i < parameters.Length; i++)
            {
                c = commonFactor(c, parameters[i]);
            }
            return c;
        }

        private int commonFactor(int a, int b)
        {
            int res = 1;
            for (int t = a; t > 0; t--)
            {
                if (a % t == 0 && b % t == 0)
                {
                    res = t;
                    break;
                }
            }
            return res;
        }

    }
}
