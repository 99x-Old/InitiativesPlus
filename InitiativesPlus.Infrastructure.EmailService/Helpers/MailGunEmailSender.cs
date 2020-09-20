using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace InitiativesPlus.Infrastructure.EmailService.Helpers
{
    public class MailgunEmailSender : IEmailSender
    {
        private readonly HttpClient _mailgunHttpClient;
        private readonly MailConfigSection _mailConfigSection;
        private readonly ILogger<MailgunEmailSender> _logger;

        public MailgunEmailSender(HttpClient mailgunHttpClient,
            MailConfigSection mailConfigSection,
            ILogger<MailgunEmailSender> logger)
        {
            this._mailgunHttpClient = mailgunHttpClient;
            this._mailConfigSection = mailConfigSection;
            this._logger = logger;
        }
        public async Task SendMailAsync(string to, string subject, string body)
        {
            Dictionary<string, string> form = new Dictionary<string, string>
            {
                ["from"] = _mailConfigSection.From,
                ["to"] = to,
                ["subject"] = subject,
                ["html"] = body,
            };

            HttpResponseMessage response = await _mailgunHttpClient.PostAsync($"v3/{_mailConfigSection.Domain}/messages", new FormUrlEncodedContent(form));

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Error when trying to send mail. mailFrom: {mailFrom}, emailTo: {emailTo}, body: {body}, subject: {subject}, response: {@response}", _mailConfigSection.From, to, body, subject, response);
            }
        }
    }
}
