using LocalizerAbstraction;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LocalizerAbstraction
    {

        public static void AddMongodbLocalizer(this IServiceCollection services, Action<MongodbLocalizerOptions> action = null)
        {
            MongodbLocalizerOptions options = new MongodbLocalizerOptions();
            action?.Invoke(options);
            services.AddSingleton(options);
            //本地序列化
            services.AddLocalization();
            services.AddSingleton<IStringLocalizerFactory, MongodbStringLocalizerFactory>();

        }
    }

}
