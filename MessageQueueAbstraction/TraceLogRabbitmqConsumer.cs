using DiagnosticModel;
using PersistenceAbstraction;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;


namespace MessageQueueAbstraction
{

    public class TraceLogRabbitmqConsumer : IRabbitmqConsumer
    {
        private readonly IRabbitmqChannelManagement _rabbitmqChannelManagement;
        private readonly IPersistence _persistence;
        public TraceLogRabbitmqConsumer(IRabbitmqChannelManagement rabbitmqChannelManagement, Func<string, IPersistence> func)
        {
            _rabbitmqChannelManagement = rabbitmqChannelManagement;
            _persistence = func.Invoke("FreeSql");
        }
        public const string NAME = "MicroService.TraceLog";
        public string Name { get { return NAME; } }

        public void Subscripe()
        {
            var channel = _rabbitmqChannelManagement.GetChannel(Name);
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, args) =>
            {
                var message = Encoding.UTF8.GetString(args.Body.Span);
                var loginfo = message.ToObj<LogInfo>();
                if (loginfo != null)
                {
                    _persistence.InsertAsync(loginfo);
                }
                channel.BasicAck(args.DeliveryTag, false);
            };

            channel.BasicConsume(Name, false, consumer);
        }


    }
}
