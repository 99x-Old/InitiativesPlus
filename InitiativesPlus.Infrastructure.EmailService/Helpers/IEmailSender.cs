using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InitiativesPlus.Infrastructure.EmailService.Helpers
{
    public interface IEmailSender
    {
        Task SendMailAsync(string to, string subject, string body);
    }
}
