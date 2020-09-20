using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using InitiativesPlus.Infrastructure.EmailService.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InitiativesPlus.Infrastructure.EmailService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            MailConfigSection mailConfigSection = Configuration.GetSection("MailGunConfigSection").Get<MailConfigSection>();

            services.AddSingleton(mailConfigSection);

            services.AddHttpClient<IEmailSender, MailgunEmailSender>(config =>
            {
                config.BaseAddress = new Uri("https://api.mailgun.net");
                config.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes($"api:{mailConfigSection.MailgunKey}")));
            });
            services.AddHostedService<NotificationService>();
            services.AddSingleton<ISubscriptionClient>(x =>
                new SubscriptionClient(Configuration.GetValue<string>("ServiceBus:ConnectionString"),
                    Configuration.GetValue<string>("ServiceBus:TopicName"),
                    Configuration.GetValue<string>("ServiceBus:SubscriptionName")));
            //services.AddScoped<IEmailSender, MailgunEmailSender>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Method intentionally left empty.
        }
    }
}
