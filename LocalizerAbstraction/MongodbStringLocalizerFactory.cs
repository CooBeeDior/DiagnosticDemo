using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizerAbstraction
{
 
    public class MongodbStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostingEnvironment _hostingEnvironment;
        public MongodbStringLocalizerFactory(IServiceProvider serviceProvider, IHostingEnvironment hostingEnvironment)
        {
            _serviceProvider = serviceProvider;
            _hostingEnvironment = hostingEnvironment;
        }
        public IStringLocalizer Create(Type resourceSource)
        {
            return new MongodbStringLocalizer(resourceSource.FullName, _serviceProvider);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new MongodbStringLocalizer($"{baseName}.{location}", _serviceProvider);
        }
    }
}
