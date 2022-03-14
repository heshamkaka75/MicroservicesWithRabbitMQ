using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RMQConsumer
{
    public class MyBackgroundService : BackgroundService
    {
        private Timer timer;
        public MyBackgroundService()
        {
        }

        public static List<Policy> Polies = new List<Policy>()
        {
          new Policy {Id = 1,Name = "ali",Email = "ali@gmail.com",ExpairDate = DateTime.Now},
          new Policy {Id = 2,Name = "mona",Email = "mona@gmail.com",ExpairDate = DateTime.Now},
          new Policy {Id = 3,Name = "ahmed",Email = "ahmed@gmail.com",ExpairDate = DateTime.Now}

        };

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            

            foreach (var Policy in Polies)
            {
                try
                {
                    timer = new Timer(o =>
                    //await SendEmailAsync({Policy.Email},"Concord Leasing - Policy Expiry Reminder",$"Dear {Policy.Name} your Policy will end 30 days");
                    Console.WriteLine($"Dear {Policy.Name} your Policy will end in {Policy.ExpairDate} - Email: {Policy.Email}"),
                    //RMQComsumer.Consume(channel),
                    null,
                    TimeSpan.Zero,
                    TimeSpan.FromDays(1));
                }
                catch (Exception ex)
                {
                    //await SendEmailAsync("auto@oasisoft.net","Email Expiry Reminder not sent",$"Policy Info... ex.Message");
                    Console.WriteLine($"Email not sent to {Policy.Name} - Email: {Policy.Email}");
                }
                
            }

            return Task.CompletedTask;
        }
    }
}
