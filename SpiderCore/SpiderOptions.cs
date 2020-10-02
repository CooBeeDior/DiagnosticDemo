using System;
using System.Collections.Generic;
using System.Text;

namespace SpiderCore
{
    public class SpiderOptions
    {
        public SpiderOptions()
        {
            Services = new List<SpiderService>();
        }

        /// <summary>
        /// 全局 返回200就表示成功
        /// </summary>
        public string HealthUrl { get; set; } = "/health";
        /// <summary>
        /// 全局间隔时间5秒
        /// </summary>
        public int IntervalTime { get; set; } = 5;
 
        public IList<SpiderService> Services { get; set; }
    }



    public class SpiderService
    {
        public SpiderService(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }
            ServiceEntryies = new List<SpiderServiceEntry>();
            ServiceName = serviceName;
        }
        /// <summary>
        /// 自定义轮询算法类型
        /// </summary>
        public Type ArithmeticType { get; set; }

        /// <summary>
        /// 请求策略类型 默认：轮询
        /// </summary>
        public StrategyType StrategyType { get; set; } = StrategyType.RoundRobin;

        public string ServiceName { get; }


        /// <summary>
        /// 返回200就表示成功
        /// </summary>
        public string HealthUrl { get; set; }  
        /// <summary>
        /// 间隔时间5秒
        /// </summary>
        public int IntervalTime { get; set; } 


        public IList<SpiderServiceEntry> ServiceEntryies { get; }

    }

    public class SpiderServiceEntry
    {
        public SpiderServiceEntry(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            this.Url = url;
        }
        /// <summary>
        /// 是否健康 注：后期可以采用注册中心去动态配置
        /// </summary>
        public bool IsHealth { get; set; } = true;
        /// <summary>
        /// 请求Url 
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// 权重 默认：1
        /// </summary>
        public int Weight { get; set; } = 1;

        public override bool Equals(object obj)
        {
            if (obj == null) {
                return false;
            }
            var target = obj as SpiderServiceEntry;
            if (obj == null)
            {
                return false;
            }
            return target.Url==this.Url;
        }

        public override int GetHashCode()
        {
            return this.Url.GetHashCode();
        }

    }

    public enum StrategyType
    {
        /// <summary>
        ///轮询 
        /// </summary>
        RoundRobin,
        /// <summary>
        /// 加权轮询
        /// </summary>
        WeightRoundRobin,
        /// <summary>
        /// 加权随机
        /// </summary>
        WeightRandom,
        /// <summary>
        /// 随机
        /// </summary>
        Random,
        /// <summary>
        /// 源地址哈希
        /// </summary>
        OrignIpHash,
        /// <summary>
        /// 最小连接
        /// </summary>
        //MinimumConnection,
        /// <summary>
        /// 自定义
        /// </summary>
        Custom


    }
}
