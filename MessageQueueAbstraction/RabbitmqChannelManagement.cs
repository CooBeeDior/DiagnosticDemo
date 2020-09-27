using RabbitMQ.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MessageQueueAbstraction
{
    public class RabbitmqChannelManagement : IRabbitmqChannelManagement
    {
        private static ConcurrentDictionary<IRabbitmqConsumer, IModel> channels = new ConcurrentDictionary<IRabbitmqConsumer, IModel>();
        private IConnection _connection;
        public RabbitmqChannelManagement(IConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.CreateConnection();
        }
        public IModel Declare(IRabbitmqConsumer consumer)
        {
            if (channels.Any(o => o.Key.GetType() == consumer.GetType()))
            {
                throw new Exception($"already exsit {consumer}");
            }
            var channel = _connection.CreateModel();
            channel.QueueDeclare(consumer.Name);
            channels.TryAdd(consumer, channel);
            return channel;
        }


        public Dictionary<IRabbitmqConsumer, IModel> GetChannels()
        {
            return new Dictionary<IRabbitmqConsumer, IModel>(channels);
        }

        public IModel GetChannel(string name)
        {
            var channel = channels.Where(o => o.Key.Name == name).FirstOrDefault();
            return channel.Value;

        }
    }
}
