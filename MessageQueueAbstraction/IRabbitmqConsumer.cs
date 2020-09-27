using System;
using System.Collections.Generic;
using System.Text;

namespace MessageQueueAbstraction
{
    public interface IRabbitmqConsumer
    {
        string Name { get; }
        void Subscripe();
    }
}
