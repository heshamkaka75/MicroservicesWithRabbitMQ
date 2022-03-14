using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Threading;

namespace RMQProducer
{
    public static class RMQProducer
    {
        public static void Publish(IModel channel)
        {
            
            //channel.ExchangeDeclare("demo-exchange", ExchangeType.Direct);
            channel.QueueDeclare("demo-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

          
                foreach (var p in MyBackgroundService.Polies)
                {
                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(p.Name));
                    channel.BasicPublish("", "demo-queue", null, body);
                }
        }

    }

}
