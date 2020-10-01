using System;
using System.Collections.Generic;
using System.Text;

namespace SpiderCore
{
    public class SpiderOptions
    {
        public SpiderOptions()
        {
            Services = new List<SpiderService>();
        }
        public IList<SpiderService> Services { get; set; }
    }


    public class SpiderService
    {

        public string ServiceName { get; set; }


        public IList<SpiderServiceEntry> ServiceEntry { get; }

    }

    public class SpiderServiceEntry
    {
        /// <summary>
        /// 请求Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 权重
        /// </summary>
        public int Weight { get; set; }

    }
}
