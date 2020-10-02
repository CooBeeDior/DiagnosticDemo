using System;
using System.Collections.Generic;
using System.Text;

namespace SpiderCore
{
    [AttributeUsageAttribute(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class RequestStrategyAttribute : Attribute
    {
        public RequestStrategyAttribute(StrategyType strategyType)
        {
            StrategyType = strategyType;
        }
        public StrategyType StrategyType { get; } = StrategyType.RoundRobin;
    }
}
