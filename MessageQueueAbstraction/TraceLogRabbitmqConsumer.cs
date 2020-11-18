using DiagnosticModel;
using TransPortServiceAbstraction;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;


namespace MessageQueueAbstraction
{

    public class TraceLogRabbitmqConsumer : IRabbitmqConsumer
    {
        private readonly IRabbitmqChannelManagement _rabbitmqChannelManagement;
        private readonly ITransPortService _transPortService;
        public TraceLogRabbitmqConsumer(IRabbitmqChannelManagement rabbitmqChannelManagement, Func<string, ITransPortService> func)
        {
            _rabbitmqChannelManagement = rabbitmqChannelManagement;
            _transPortService = func.Invoke("Mongodb");
        }
        public const string NAME = "MicroService.TraceLog";
        public string Name { get { return NAME; } }

        public void Subscripe()
        {
            var channel = _rabbitmqChannelManagement.GetChannel(Name);
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (sender, args) =>
            {
                var message = Encoding.UTF8.GetString(args.Body.Span);
                var traceInfo = message.ToObj<TraceInfo>();
                if (traceInfo != null)
                {
                    await _transPortService.Send(traceInfo);
                }
                channel.BasicAck(args.DeliveryTag, false);
            };

            channel.BasicConsume(Name, false, consumer);
        }


    }
}
