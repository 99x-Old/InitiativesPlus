using InitiativesPlus.Application.Interfaces;
using InitiativesPlus.Application.Services;
using InitiativesPlus.Domain.Interfaces;
using InitiativesPlus.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace InitiativesPlus.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //InitiativesPlus.Application
            services.AddScoped<IInitiativeService, InitiativeService>();

            //InitiativesPlus.Domain.Interfaces | InitiativesPlus.Infrastructure.Data.Repositories
            services.AddScoped<IInitiativeRepository, InitiativeRepository>();
        }
    }
}
