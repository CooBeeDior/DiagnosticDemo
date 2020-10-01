using System;
using System.Linq;

namespace SpiderCore.RequestStrategies
{
    public class RequestStrategyBase
    {

        protected SpiderService SpiderService;
        public RequestStrategyBase(SpiderService spiderService)
        {
            if (spiderService == null || spiderService.ServiceEntryies == null || !spiderService.ServiceEntryies.Any())
            {
                throw new ArgumentNullException(nameof(spiderService));
            }
            SpiderService = spiderService;
        }
    }
}
