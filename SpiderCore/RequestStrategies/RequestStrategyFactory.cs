using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Net;
using Microsoft.Extensions.Logging;
using DiagnosticModel;

namespace SpiderCore.RequestStrategies
{


    public interface IMonitorHealthJob
    {
        Task StartHealthJobAsync();
    }
    public class MonitorHealthJob : IMonitorHealthJob
    {
        private readonly SpiderOptions _spiderOptions;
        private readonly HttpClient _httpClient;
        private readonly ILogger<MonitorHealthJob> _logger;
        public MonitorHealthJob(SpiderOptions spiderOptions, IHttpClientFactory httpClientFactory, ILogger<MonitorHealthJob> logger)
        {
            _spiderOptions = spiderOptions;
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;
        }
        public virtual Task StartHealthJobAsync()
        {
            foreach (var service in _spiderOptions.Services)
            {
                int intervalTime = service.IntervalTime == 0 ? _spiderOptions.IntervalTime : service.IntervalTime;
                int hour = intervalTime / 3600;
                int minute = intervalTime % 3600 / 60;
                int second = intervalTime % 3600 % 60;
                string cron = $"*/{second} * * * * ?";
                RecurringJob.AddOrUpdate(() => RefreshService(service), cron);
            }

            return Task.CompletedTask;
        }

        public virtual void CheckServiceStatus(SpiderService spiderService)
        {
            string healthUrl = string.IsNullOrWhiteSpace(spiderService.HealthUrl) ? _spiderOptions.HealthUrl : spiderService.HealthUrl;
            if (spiderService != null && spiderService.ServiceEntryies != null)
            {
                //foreach (var serviceEntry in spiderService.ServiceEntryies)
                for (int i = 0; i < spiderService.ServiceEntryies.Count; i++)
                {
                    var serviceEntry = spiderService.ServiceEntryies[i];
                    var url = Url.Combine(serviceEntry.Url, healthUrl);
                    try
                    {
                        _httpClient.DefaultRequestHeaders.Add("health", "1");
                        var resp = _httpClient.GetAsync(url).GetAwaiter().GetResult();
                        if (resp.StatusCode == HttpStatusCode.OK)
                        {
                            serviceEntry.IsHealth = true;
                        }
                        else
                        {
                            serviceEntry.IsHealth = false;
                        }
                        _logger.LogTrace($"检查【{spiderService.ServiceName}】服务状态，地址{url}，状态：{serviceEntry.IsHealth}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, $"{spiderService.ServiceName}服务健康检查，请求{url}报错");
                        serviceEntry.IsHealth = false;
                    }
                }
            }
        }

        public void RefreshService(SpiderService spiderService)
        {
            CheckServiceStatus(spiderService);

            List<IRequestStrategy> requestStrategies = new List<IRequestStrategy>();

            if (RequestStrategyFactory._systemStrategyType.TryGetValue(spiderService.ServiceName, out IList<(StrategyType, IRequestStrategy)> systemStrategies))
            {
                requestStrategies.AddRange(systemStrategies.Select(o => o.Item2));
            }
            if (RequestStrategyFactory._customStrategyType.TryGetValue(spiderService.ServiceName, out IList<(Type, IRequestStrategy)> customStrategies))
            {
                requestStrategies.AddRange(customStrategies.Select(o => o.Item2));
            }
            foreach (var item in requestStrategies)
            {
                item.RefreshHealthService(spiderService.ServiceEntryies.Where(o => o.IsHealth).ToList());
            }
        }
    }

    public static class RequestStrategyExtensions
    {

        public static void UseMonitorHealthJob(this IApplicationBuilder app)
        {
            var monitorHealth = app.ApplicationServices.GetService<IMonitorHealthJob>();
            monitorHealth.StartHealthJobAsync().GetAwaiter().GetResult();
        }
    }


    public class RequestStrategyFactory : IRequestStrategyFactory
    {
        public static ConcurrentDictionary<string, IList<(StrategyType, IRequestStrategy)>> _systemStrategyType = new ConcurrentDictionary<string, IList<(StrategyType, IRequestStrategy)>>();
        public static ConcurrentDictionary<string, IList<(Type, IRequestStrategy)>> _customStrategyType = new ConcurrentDictionary<string, IList<(Type, IRequestStrategy)>>();



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

                    var systemRequestStrategies = _systemStrategyType.GetOrAdd(spiderService.ServiceName, serviceName =>
                    {
                        return new List<(StrategyType, IRequestStrategy)>();
                    });
                    var systemRequestStrategyTuple = systemRequestStrategies.FirstOrDefault(o => o.Item1 == spiderService.StrategyType);
                    if (systemRequestStrategyTuple.Item2 == null)
                    {
                        var types = this.GetType().Assembly.GetTypes().Where(o => typeof(IRequestStrategy).IsAssignableFrom(o) && !o.IsAbstract && o.IsClass);
                        var type = types.Where(o => o.GetCustomAttribute<RequestStrategyAttribute>().StrategyType == spiderService.StrategyType).FirstOrDefault();
                        var strategy = Activator.CreateInstance(type, spiderService) as IRequestStrategy;
                        if (strategy == null)
                        {
                            throw new Exception($"not found {spiderService.StrategyType} service");
                        }
                        requestStrategy = strategy;
                        systemRequestStrategies.Add((spiderService.StrategyType, requestStrategy));
                    }
                    else
                    {
                        requestStrategy = systemRequestStrategyTuple.Item2;
                    }
                    break;
                case StrategyType.Custom:
                    var customRequestStrategies = _customStrategyType.GetOrAdd(spiderService.ServiceName, serviceName =>
                    {
                        return new List<(Type, IRequestStrategy)>();
                    });
                    var customRequestStrategyTuple = customRequestStrategies.FirstOrDefault(o => o.Item1 == spiderService.ArithmeticType);
                    if (customRequestStrategyTuple.Item2 == null)
                    {
                        var strategy = Activator.CreateInstance(spiderService.ArithmeticType, spiderService) as IRequestStrategy;
                        if (strategy == null)
                        {
                            throw new Exception($"not found {spiderService.StrategyType} service");
                        }
                        requestStrategy = strategy;
                        customRequestStrategies.Add((spiderService.ArithmeticType, requestStrategy));
                    }
                    else
                    {
                        requestStrategy = customRequestStrategyTuple.Item2;
                    }
                    break;


            }
            return requestStrategy;
        }
    }
}
