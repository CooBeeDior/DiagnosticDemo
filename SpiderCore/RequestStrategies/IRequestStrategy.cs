using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SpiderCore.RequestStrategies
{
    public interface IRequestStrategy
    {
        string GetServiceIp(object param);
    }


}
