using System;
using System.Collections.Generic;
using System.Text;

namespace RateLimitingCore
{
    public struct RateLimitCounter
    {
        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// 请求的数量
        /// </summary>
        public double Count { get; set; }
    }
}
