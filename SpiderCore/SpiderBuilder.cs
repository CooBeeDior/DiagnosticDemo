using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpiderCore
{
    public class SpiderBuilder : ISpiderBuilder
    {
        private SpiderOptions _spiderOptions;
        public SpiderBuilder(SpiderOptions spiderOptions)
        {
            if (spiderOptions == null || spiderOptions.Services == null)
            {
                throw new ArgumentNullException(nameof(spiderOptions));
            }
            _spiderOptions = spiderOptions;
        }



        public ISpiderBuilder AddService(string name, IList<SpiderServiceEntry> serviceEntries)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (serviceEntries == null || !serviceEntries.Any())
            {
                throw new ArgumentNullException(nameof(serviceEntries));
            }
            var service = _spiderOptions.Services.Where(o => o.ServiceName.Equals(name)).FirstOrDefault();
            if (service != null)
            {
                service = new SpiderService();
            }

            foreach (var item in serviceEntries)
            {
                service.ServiceEntryies.Add(item);
            }
            _spiderOptions.Services.Add(service);
            return this;

        }

        public ISpiderBuilder AddService(string name, Action<IList<SpiderServiceEntry>> action)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            var service = getSpiderService(name);
            action(service.ServiceEntryies);
            _spiderOptions.Services.Add(service);
            return this;

        }

        public ISpiderBuilder AddServiceStrategyType(string name, Action<SpiderService> action)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            var service = getSpiderService(name);
            action(service);
            return this;

        }

        private SpiderService getSpiderService(string name)
        {
            var service = _spiderOptions.Services.Where(o => o.ServiceName.Equals(name)).FirstOrDefault();
            if (service == null)
            {
                throw new Exception($"{name} not exsit");
            }
            return service;


        }
    }
}
