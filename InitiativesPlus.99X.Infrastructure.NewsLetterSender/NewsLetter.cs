using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace InitiativesPlus._99X.Infrastructure.NewsLetterSender
{
    public static class NewsLetter
    {
        private static HttpClient httpClient = new HttpClient();

        [FunctionName("NewsLetter")]
        public static async Task Run([TimerTrigger("0 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            var user = new User
            {
                Username = "superadmin",
                Password = "password"
            };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://initiatives-plus-api.azurewebsites.net";
            using var client = new HttpClient();
            // https://initiatives-plus-api.azurewebsites.net/
            var response = await client.PostAsync(url + "/api/user/login", data);

            string result = await response.Content.ReadAsStringAsync();
            log.LogInformation($"Token: {result}");

        }
    }

    class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
