using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InitiativesPlus.Infrastructure.EmailService.Helpers;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace InitiativesPlus.Infrastructure.EmailService
{
    public class NotificationService : BackgroundService
    {
        private readonly ISubscriptionClient _subscriptionClient;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public NotificationService(
            ISubscriptionClient subscriptionClient, 
            IServiceScopeFactory serviceScopeFactory)
        {
            _subscriptionClient = subscriptionClient;
            _serviceScopeFactory = serviceScopeFactory;

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IEmailSender>();
                _subscriptionClient.RegisterMessageHandler((message, token) =>
                {
                    var messageRecieved =
                        JsonConvert.DeserializeObject<ServiceBusMessage>(Encoding.UTF8.GetString(message.Body));
                    var messageContent =
                        JsonConvert.DeserializeObject<MessageBody>(messageRecieved.Content);

                    Console.WriteLine($"New message with content {messageRecieved.Content} and id {messageRecieved.Id} to {messageContent.Email}");
                    scopedProcessingService.SendMailAsync(messageContent.Email, messageRecieved.Type, messageContent.Body);

                    return _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                }, new MessageHandlerOptions(args => Task.CompletedTask)
                {
                    AutoComplete = false,
                    MaxConcurrentCalls = 1
                });
            }

            return Task.CompletedTask;
        }

        public class ServiceBusMessage
        {
            public string Id { get; set; }
            public string Type { get; set; }
            public string Content { get; set; }
        }

        public class MessageBody
        {
            public string Body { get; set; }
            public string Email { get; set; }
        }
    }
}
