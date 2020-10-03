using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizerAbstraction
{
    public class MongodbLocalizerOptions
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string CollectionName { get; set; }
    }

}
