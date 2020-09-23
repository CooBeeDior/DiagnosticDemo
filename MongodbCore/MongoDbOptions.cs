using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongodbCore
{
    public class MongoDbOptions  
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string ConnectionName { get; set; }

 
    }
}
