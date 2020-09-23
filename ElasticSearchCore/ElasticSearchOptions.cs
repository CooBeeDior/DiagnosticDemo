using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchCore
{
    public class ElasticSearchOptions 
    {
        public IList<string> Urls { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int NumberOfReplicas { get; set; } = 1;

        public int NumberOfShards { get; set; } = 1;


    }
}
