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
        public IList<SpiderService> Services { get; set; }
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

    public class SpiderService
    {
        public SpiderService()
        {
            ServiceEntryies = new List<SpiderServiceEntry>();
        }
        /// <summary>
        /// 自定义轮询算法类型
        /// </summary>
        public Type ArithmeticType { get; set; }

        /// <summary>
        /// 请求策略类型 默认：轮询
        /// </summary>
        public StrategyType StrategyType { get; set; } = StrategyType.RoundRobin;

        public string ServiceName { get; set; }


        public IList<SpiderServiceEntry> ServiceEntryies { get; }

    }

    public class SpiderServiceEntry
    {
        /// <summary>
        /// 是否健康 注：后期可以采用注册中心去动态配置
        /// </summary>
        public bool IsHealth { get; set; } = true;
        /// <summary>
        /// 请求Url 
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 权重 默认：1
        /// </summary>
        public int Weight { get; set; } = 1;

    }
}
