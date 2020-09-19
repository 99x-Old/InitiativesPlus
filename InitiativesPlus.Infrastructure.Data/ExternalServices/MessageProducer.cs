using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace InitiativesPlus.Infrastructure.Data.ExternalServices
{
    public class MessageProducer
    {
        static ITopicClient _topicClient;

        public static async Task SendMessageAsync(string messageType, string email, string messageBody)
        {
            string ServiceBusConnectionString = "Endpoint=sb://notification-ennoblex.servicebus.windows.net/;SharedAccessKeyName=send-seb-key;SharedAccessKey=t8lydeQo28thsjeLWNXaTeeJYprvmtq5Ym2R/9cAMHg= ";
            string TopicName = "role-change";

            _topicClient = new TopicClient(ServiceBusConnectionString, TopicName);

            // Send messages.
            await SendUserMessageAsync(messageType, email, messageBody);
            await _topicClient.CloseAsync();
        }

        private static async Task SendUserMessageAsync(string messageType, string email, string messageBody)
        {
            string messageId = Guid.NewGuid().ToString();

            var content = new MessageBody
            {
                Body = messageBody,
                Email = email
            };

            var serializedContent = JsonConvert.SerializeObject(content);
            var message = new ServiceBusMessage
            {
                Id = messageId,
                Type = messageType,
                Content = serializedContent
            };

            var serializeBody = JsonConvert.SerializeObject(message);

            // send data to bus
            var busMessage = new Message(Encoding.UTF8.GetBytes(serializeBody));
            busMessage.UserProperties.Add("Type", messageType);
            busMessage.MessageId = messageId;

            await _topicClient.SendAsync(busMessage);

            Console.WriteLine("message has been sent");

        }

        public class MessageBody
        {
            public string Body { get; set; }
            public string Email { get; set; }
        }

        public class ServiceBusMessage
        {
            public string Id { get; set; }
            public string Type { get; set; }
            public string Content { get; set; }
        }
    }
}
