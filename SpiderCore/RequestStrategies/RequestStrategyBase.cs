using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace SpiderCore.RequestStrategies
{
    public class RequestStrategyBase
    {
        public IList<SpiderServiceEntry> HealthServices { get; set; }

        protected readonly SpiderService SpiderService;


        public RequestStrategyBase(SpiderService spiderService)
        {
            if (spiderService == null || spiderService.ServiceEntryies == null || !spiderService.ServiceEntryies.Any())
            {
                throw new ArgumentNullException(nameof(spiderService));
            }
            SpiderService = spiderService;
            HealthServices = spiderService.ServiceEntryies.Where(o => o.IsHealth).ToList();
        }


        public virtual void RefreshHealthService(IList<SpiderServiceEntry> healthService)
        {
            HealthServices = healthService;
        }
    }
}
