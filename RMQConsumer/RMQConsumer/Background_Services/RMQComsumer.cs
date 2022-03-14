
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RMQConsumer
{
    public static class RMQComsumer
    {
        public static List<Policy> LPolies = new List<Policy>()
        {
          new Policy {Id = 1,Name = "ali",Email = "ali@gmail.com",ExpairDate = DateTime.Now},
          new Policy {Id = 2,Name = "mona",Email = "mona@gmail.com",ExpairDate = DateTime.Now},
          new Policy {Id = 3,Name = "ahmed",Email = "ahmed@gmail.com",ExpairDate = DateTime.Now}

        };

        public static void Consume(IModel channel)
    {
        //channel.ExchangeDeclare("demo-exchange", ExchangeType.Direct);
        channel.QueueDeclare("demo-queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (sender, e) =>
        {
            var body = e.Body.ToArray();

            var message = Encoding.UTF8.GetString(body);

            string x = message.Trim('"');

            var y = LPolies.SingleOrDefault(a => a.Name == x);
            Console.WriteLine(y.Id + y.Name + y.Email + y.ExpairDate);
        };

        //channel.QueueBind("demo-queue", "demo-exchange", "SendReminder");

        channel.BasicConsume("demo-queue", true, consumer);
        Console.WriteLine("Start Consume");
        Console.ReadLine();
    }
}
}
