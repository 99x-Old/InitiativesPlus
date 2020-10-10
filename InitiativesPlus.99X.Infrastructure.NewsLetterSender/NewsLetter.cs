using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace InitiativesPlus._99X.Infrastructure.NewsLetterSender
{
    public static class NewsLetter
    {
        private static readonly string url = "https://initiatives-plus-api.azurewebsites.net";

        [FunctionName("NewsLetter")]
        public static async Task Run([TimerTrigger("0 0 0 1 * *")]TimerInfo myTimer, ILogger log) // Runs every 1st of the month
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            var user = new User
            {
                Username = "superadmin",
                Password = "password"
            };

            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var response = await client.PostAsync(url + "/api/user/login", data);

            var result = await response.Content.ReadAsAsync<Token>();

            List<string> emails = await GetListOfEmailsAsync(result.tokenString);
            List<string> events = await GetEventsAsync(result.tokenString, log);

            if (events.Count > 0)
            {
                string list = String.Join(events.Count == 1 ? "" : ", ", events);

                foreach (string email in emails)
                {
                    log.LogInformation($"Sending to : {email}");
                    await MessageProducer.SendMessageAsync(
                        $"InitiativesPLUS Newsletter for " + DateTime.Now.ToString("MMMM"),
                        email, $"<!doctype html><html> <head> <meta name=\"viewport\" content=\"width=device-width\" /> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /> <style> /* ------------------------------------- GLOBAL RESETS ------------------------------------- */ /*All the styling goes here*/ img {{ border: none; -ms-interpolation-mode: bicubic; max-width: 100%; }} body {{ background-color: #f6f6f6; font-family: sans-serif; -webkit-font-smoothing: antialiased; font-size: 14px; line-height: 1.4; margin: 0; padding: 0; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; }} table {{ border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; }} table td {{ font-family: sans-serif; font-size: 14px; vertical-align: top; }} /* ------------------------------------- BODY & CONTAINER ------------------------------------- */ .body {{ background-color: #f6f6f6; width: 100%; }} /* Set a max-width, and make it display as block so it will automatically stretch to that width, but will also shrink down on a phone or something */ .container {{ display: block; margin: 0 auto !important; /* makes it centered */ max-width: 580px; padding: 10px; width: 580px; }} /* This should also be a block element, so that it will fill 100% of the .container */ .content {{ box-sizing: border-box; display: block; margin: 0 auto; max-width: 580px; padding: 10px; }} /* ------------------------------------- HEADER, FOOTER, MAIN ------------------------------------- */ .main {{ background: #ffffff; border-radius: 3px; width: 100%; }} .wrapper {{ box-sizing: border-box; padding: 20px; }} .content-block {{ padding-bottom: 10px; padding-top: 10px; }} .footer {{ clear: both; margin-top: 10px; text-align: center; width: 100%; }} .footer td, .footer p, .footer span, .footer a {{ color: #999999; font-size: 12px; text-align: center; }} /* ------------------------------------- TYPOGRAPHY ------------------------------------- */ h1, h2, h3, h4 {{ color: #000000; font-family: sans-serif; font-weight: 400; line-height: 1.4; margin: 0; margin-bottom: 30px; }} h1 {{ font-size: 35px; font-weight: 300; text-align: center; text-transform: capitalize; }} p, ul, ol {{ font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; margin-bottom: 15px; }} p li, ul li, ol li {{ list-style-position: inside; margin-left: 5px; }} a {{ color: #3498db; text-decoration: underline; }} /* ------------------------------------- BUTTONS ------------------------------------- */ .btn {{ box-sizing: border-box; width: 100%; }} .btn > tbody > tr > td {{ padding-bottom: 15px; }} .btn table {{ width: auto; }} .btn table td {{ background-color: #ffffff; border-radius: 5px; text-align: center; }} .btn a {{ background-color: #ffffff; border: solid 1px #3498db; border-radius: 5px; box-sizing: border-box; color: #3498db; cursor: pointer; display: inline-block; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-decoration: none; text-transform: capitalize; }} .btn-primary table td {{ background-color: #3498db; }} .btn-primary a {{ background-color: #3498db; border-color: #3498db; color: #ffffff; }} /* ------------------------------------- OTHER STYLES THAT MIGHT BE USEFUL ------------------------------------- */ .last {{ margin-bottom: 0; }} .first {{ margin-top: 0; }} .align-center {{ text-align: center; }} .align-right {{ text-align: right; }} .align-left {{ text-align: left; }} .clear {{ clear: both; }} .mt0 {{ margin-top: 0; }} .mb0 {{ margin-bottom: 0; }} .preheader {{ color: transparent; display: none; height: 0; max-height: 0; max-width: 0; opacity: 0; overflow: hidden; mso-hide: all; visibility: hidden; width: 0; }} .powered-by a {{ text-decoration: none; }} hr {{ border: 0; border-bottom: 1px solid #f6f6f6; margin: 20px 0; }} /* ------------------------------------- RESPONSIVE AND MOBILE FRIENDLY STYLES ------------------------------------- */ @media only screen and (max-width: 620px) {{ table[class=body] h1 {{ font-size: 28px !important; margin-bottom: 10px !important; }} table[class=body] p, table[class=body] ul, table[class=body] ol, table[class=body] td, table[class=body] span, table[class=body] a {{ font-size: 16px !important; }} table[class=body] .wrapper, table[class=body] .article {{ padding: 10px !important; }} table[class=body] .content {{ padding: 0 !important; }} table[class=body] .container {{ padding: 0 !important; width: 100% !important; }} table[class=body] .main {{ border-left-width: 0 !important; border-radius: 0 !important; border-right-width: 0 !important; }} table[class=body] .btn table {{ width: 100% !important; }} table[class=body] .btn a {{ width: 100% !important; }} table[class=body] .img-responsive {{ height: auto !important; max-width: 100% !important; width: auto !important; }} }} /* ------------------------------------- PRESERVE THESE STYLES IN THE HEAD ------------------------------------- */ @media all {{ .ExternalClass {{ width: 100%; }} .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {{ line-height: 100%; }} .apple-link a {{ color: inherit !important; font-family: inherit !important; font-size: inherit !important; font-weight: inherit !important; line-height: inherit !important; text-decoration: none !important; }} #MessageViewBody a {{ color: inherit; text-decoration: none; font-size: inherit; font-family: inherit; font-weight: inherit; line-height: inherit; }} .btn-primary table td:hover {{ background-color: #34495e !important; }} .btn-primary a:hover {{ background-color: #34495e !important; border-color: #34495e !important; }} }} </style> </head> <body class=\"\"> <table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"body\"> <tr> <td>&nbsp;</td> <td class=\"container\"> <div class=\"content\"> <!-- START CENTERED WHITE CONTAINER --> <table role=\"presentation\" class=\"main\"> <!-- START MAIN CONTENT AREA --> <tr> <td class=\"wrapper\"> <table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tr> <td> <p>Hi there!</p> <p>Here are a list of new additions this month.</p><p><ul> {list}</ul></p> </td> </tr> </table> </td> </tr> <!-- END MAIN CONTENT AREA --> </table> <!-- END CENTERED WHITE CONTAINER --> <!-- START FOOTER --> <div class=\"footer\"> <table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tr> <td class=\"content-block\"> <span class=\"apple-link\">99X, No 65, Walukarama Rd, Colombo 3</span> </td> </tr> <tr> <td class=\"content-block powered-by\"> Made with &#9825; by NishanW. </td> </tr> </table> </div> <!-- END FOOTER --> </div> </td> <td>&nbsp;</td> </tr> </table> </body></html>");
                }
            }
        }

        private static async Task<List<string>> GetListOfEmailsAsync(string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await client.GetAsync(url + "/api/user/list-emails");

            var result = await response.Content.ReadAsAsync<List<string>>();

            return result;
        }

        private static async Task<List<string>> GetEventsAsync(string token, ILogger log)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await client.GetAsync(url + "/api/Initiative/events");

            var result = await response.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<List<Event>>(result);

            List<string> initiatives= new List<string>();
            foreach (var item in items)
            {
                log.LogInformation($"Initiative {item.Initiative}");
                initiatives.Add(item.Initiative);
            }
            return initiatives;
        }
    }

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

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Token
    {
        public string tokenString { get; set; }
    }

    public class Event
    {
        [JsonProperty("initiative")]
        public string Initiative { get; set; }
    }
}
