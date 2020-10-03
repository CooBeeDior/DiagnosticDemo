using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SpiderCore;
namespace DiagnosticApiDemo.HostingStartups
{
    public class SpiderStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //持久化
            builder.ConfigureServices((context, services) =>
            {
                services.AddSpider(options =>
                {
                    {
                        var s1 = new SpiderServiceEntry("http://api.coobeedior.com") { Weight = 2 };
                        var s2 = new SpiderServiceEntry("http://47.111.87.132:8012") { Weight = 4 };
                        var spiderService = new SpiderService("wechat") { StrategyType = StrategyType.WeightRoundRobin ,HealthUrl= "index" };
                        spiderService.ServiceEntryies.Add(s1);
                        spiderService.ServiceEntryies.Add(s2);
                        options.Services.Add(spiderService);
                    }

                    //{
                    //    var s1 = new SpiderServiceEntry() { Url = "http://api.coobeedior.com", Weight = 2 };
                    //    var s2 = new SpiderServiceEntry() { Url = "http://47.111.87.132:8012", Weight = 4 };
                    //    var spiderService = new SpiderService() { ServiceName = "name", StrategyType = StrategyType.WeightRoundRobin };
                    //    spiderService.ServiceEntryies.Add(s1);
                    //    spiderService.ServiceEntryies.Add(s2);        
                    //    options.Services.Add(spiderService);

                    //}



                });
            });


        }
    }
}
