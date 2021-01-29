using System;
using System.Collections.Generic;
using System.Text;

namespace MessageQueueAbstraction
{
    public interface IRabbitmqConsumer
    {
        /// <summary>
        /// 监听的名称
        /// </summary>
        string Name { get; }
        void Subscripe();
    }
}
