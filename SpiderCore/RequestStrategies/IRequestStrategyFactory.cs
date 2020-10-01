using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpiderCore.RequestStrategies
{
    public interface IRequestStrategyFactory
    {
        IRequestStrategy CreateRequestStrategy(SpiderService spiderService);
    }

    public class RequestStrategyFactory: IRequestStrategyFactory
    {
        private static IRequestStrategyFactory _instance;
        private static object obj = new object();
        public static IRequestStrategyFactory Instance
        {
            get
            {
                if (_instance == null)
                { 
                    lock (obj)
                    {
                        if (_instance == null)
                        {
                            _instance = new RequestStrategyFactory();
                        }
                    }
                }
                return _instance;
            }
        }
        public IRequestStrategy CreateRequestStrategy(SpiderService spiderService)
        {
            IRequestStrategy requestStrategy = null;
            switch (spiderService.StrategyType)
            {
                case StrategyType.RoundRobin:
                case StrategyType.WeightRoundRobin:
                case StrategyType.WeightRandom:
                case StrategyType.Random:
                case StrategyType.OrignIpHash:
                    var types = this.GetType().Assembly.GetTypes().Where(o => typeof(IRequestStrategy).IsAssignableFrom(o) && !o.IsAbstract && o.IsClass);
                    var type = types.Where(o => o.Name.StartsWith(spiderService.StrategyType.ToString())).FirstOrDefault();
                    requestStrategy = Activator.CreateInstance(spiderService.ArithmeticType, spiderService) as IRequestStrategy;

                    break;

                case StrategyType.Custom:
                    requestStrategy = Activator.CreateInstance(spiderService.ArithmeticType, spiderService) as IRequestStrategy;
                    break;


            }
            return requestStrategy;
        }
    }
}
