using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpiderCore
{
    public interface ISpiderBuilder
    {
        ISpiderBuilder AddService(string name, IList<SpiderServiceEntry> serviceEntries);


        ISpiderBuilder AddService(string name, Action<IList<SpiderServiceEntry>> action);


        ISpiderBuilder AddServiceStrategyType(string name, Action<SpiderService> action);
  
    }

 
}