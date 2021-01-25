using System;
using System.Collections.Generic;
using System.Text;

namespace RateLimitingCore
{
    public class RateLimitingOptions
    {
        public string Name { get; set; }
        public RateLimitingOptions()
        {
            List = new List<RateLimiting>();
        }

        public IList<RateLimiting> List { get; }
    }

    public class RateLimiting
    {
        public RateLimiting()
        {
            Urls = new List<string>();
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool EnableRateLimiting { get; set; } = true;
        /// <summary>
        /// 匹配请求的url
        /// </summary>
        public IList<string> Urls { get; set; }

        /// <summary>
        /// 频率 单位（秒）
        /// </summary>
        public int Period { get; set; } = 5;

        /// <summary>
        /// 限制次数
        /// </summary>
        public int Limit { get; set; } = 10;

        /// <summary>
        /// 重试3次
        /// </summary>
        public int Retry { get; set; } = 3;

    }
}
