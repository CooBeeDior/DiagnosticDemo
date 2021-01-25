using System;
using System.Collections.Generic;
using System.Text;

namespace RateLimitingCore
{
    public struct RateLimitCounter
    {
        public DateTime Timestamp { get; set; }

        public double Count { get; set; }
    }
}
