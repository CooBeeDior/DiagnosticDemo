using System.Collections.Generic;
using System.Linq;

namespace SpiderCore.RequestStrategies
{

    /// <summary>
    /// 加权抽象
    /// </summary> 
    public abstract class WeightRequestStrategy : RequestStrategyBase
    {
        protected SpiderService TargetSpiderService { get; }
        public WeightRequestStrategy(SpiderService spiderService) : base(spiderService)
        {
            SpiderService targetSpiderService = new SpiderService(spiderService.ServiceName)
            {
                ArithmeticType = spiderService.ArithmeticType,
                StrategyType = spiderService.StrategyType

            };

            var weights = HealthServices.Select(o => o.Weight).ToArray();
            int maxFactor = maxCommonFactor(weights);

            foreach (var item in HealthServices)
            {
                int num = item.Weight / maxFactor;
                for (int i = 0; i < num; i++)
                {
                    SpiderServiceEntry spiderServiceEntry = new SpiderServiceEntry(item.Url)
                    {
                        Weight = item.Weight,
                        IsHealth = item.IsHealth
                    };
                    targetSpiderService.ServiceEntryies.Add(spiderServiceEntry);
                }
            }
            TargetSpiderService = targetSpiderService;
            HealthServices = targetSpiderService.ServiceEntryies;
        }

        public abstract IRequestStrategy GetRequestStrategy();


        public override void RefreshHealthService(IList<SpiderServiceEntry> healthServices)
        {
            foreach (var healthService in healthServices)
            {
                var filterServices = TargetSpiderService.ServiceEntryies.Where(service => service == healthService).ToList();
                foreach (var filterService in filterServices)
                {
                    filterService.IsHealth = healthService.IsHealth;
                }
            }
            base.RefreshHealthService(healthServices);
        }


        #region private

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
        #endregion

    }
}
