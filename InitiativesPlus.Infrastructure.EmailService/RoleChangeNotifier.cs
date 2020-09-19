using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace InitiativesPlus.Infrastructure.EmailService
{
    public class RoleChangeNotifier : BackgroundService
    {
        private readonly ISubscriptionClient _subscriptionClient;

        public RoleChangeNotifier(ISubscriptionClient subscriptionClient)
        {
            _subscriptionClient = subscriptionClient;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriptionClient.RegisterMessageHandler((message, token) =>
            {
                var messageRecieved =
                    JsonConvert.DeserializeObject<ServiceBusMessage>(Encoding.UTF8.GetString(message.Body));
                //var messageContent =
                //    JsonConvert.DeserializeObject<ServiceBusMessage>(Encoding.UTF8.GetString(messageRecieved.Content));

                Console.WriteLine($"New message with content {messageRecieved.Content} and id {messageRecieved.Id}");

                return _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
            }, new MessageHandlerOptions(args => Task.CompletedTask)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1
            });
            return Task.CompletedTask;
        }

        public class ServiceBusMessage
        {
            public string Id { get; set; }
            public string Type { get; set; }
            public string Content { get; set; }
        }
    }
}
