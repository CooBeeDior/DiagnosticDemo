using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace DiagnosticApiDemo.HostingStartups
{
    public class MongodbLocalizerStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                //本地化  需要初始化数据
                services.AddMongodbLocalizer(options =>
                {
                    options.ConnectionString = "mongodb://coobeedior.com:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false";
                    options.DatabaseName = "diagnosticapi";
                    options.CollectionName = "localizer";
                });
                services.AddSingleton<IStartupFilter, MongodbLocalizerStartupFilter>();
            });
        }
    }

    public class MongodbLocalizerStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                //英文
                new CultureInfo("en-US"),
                //中文
                new CultureInfo("zh-CN"),
            };
                //添加本地化机制
                app.UseRequestLocalization(new RequestLocalizationOptions
                {
                    DefaultRequestCulture = new RequestCulture("en-US"),

                    SupportedCultures = supportedCultures,
                    SupportedUICultures = supportedCultures
                });

                next(app);
            };
        }
    }

}
