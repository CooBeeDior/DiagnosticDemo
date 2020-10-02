using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpiderCore.RequestStrategies
{
    public interface IRequestStrategyFactory
    {
        IRequestStrategy CreateRequestStrategy(SpiderService spiderService);
    }


}
