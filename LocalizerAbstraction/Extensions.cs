using LocalizerAbstraction;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {

        public static void AddMongodbLocalizer(this IServiceCollection services, Action<MongodbLocalizerOptions> action = null)
        {
            MongodbLocalizerOptions options = new MongodbLocalizerOptions();
            action?.Invoke(options);
            services.AddSingleton(options);
            //本地序列化
            services.AddLocalization(o =>
            {
                o.ResourcesPath = "Resources";
            });
            services.AddSingleton<IStringLocalizerFactory, MongodbStringLocalizerFactory>();

        }
    }

}
