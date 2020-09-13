﻿using AutoMapper;
using InitiativesPlus.Application.Interfaces;
using InitiativesPlus.Application.MappingProfile;
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
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            //InitiativesPlus.Application
            services.AddScoped<IInitiativeService, InitiativeService>();
            services.AddScoped<IAuthService, AuthService>();

            //InitiativesPlus.Domain.Interfaces | InitiativesPlus.Infrastructure.Data.Repositories
            services.AddScoped<IInitiativeRepository, InitiativeRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
        }
    }
}
