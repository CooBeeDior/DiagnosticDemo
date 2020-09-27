using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageQueueAbstraction
{
    public interface IRabbitmqChannelManagement
    {
        IModel Declare(IRabbitmqConsumer consumer);



        Dictionary<IRabbitmqConsumer, IModel> GetChannels();


        IModel GetChannel(string name);
    }
}
