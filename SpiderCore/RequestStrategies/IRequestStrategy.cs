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
        /// <summary>
        /// 健康服务地址
        /// </summary>
        public IList<SpiderServiceEntry> HealthServices { get; }
        /// <summary>
        /// 获取Ip
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        string GetServiceIp(object param = null);
        /// <summary>
        /// 更新服务状态
        /// </summary>
        /// <param name="healthServices"></param>
        void RefreshHealthService(IList<SpiderServiceEntry> healthServices);
    }


}
