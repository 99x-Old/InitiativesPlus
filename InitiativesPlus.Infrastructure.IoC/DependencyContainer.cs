using AutoMapper;
using InitiativesPlus.Application.Interfaces;
using InitiativesPlus.Application.MappingProfile;
using InitiativesPlus.Application.Services;
using InitiativesPlus.Domain.Interfaces;
using InitiativesPlus.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace InitiativesPlus.Infrastructure.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles));

            //InitiativesPlus.Application
            services.AddScoped<IInitiativeService, InitiativeService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();

            //InitiativesPlus.Domain.Interfaces | InitiativesPlus.Infrastructure.Data.Repositories
            services.AddScoped<IInitiativeRepository, InitiativeRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
        }
    }
}
