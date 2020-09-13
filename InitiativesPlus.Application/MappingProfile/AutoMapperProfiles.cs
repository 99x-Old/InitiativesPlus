using AutoMapper;
using InitiativesPlus.Application.ViewModels;
using InitiativesPlus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace InitiativesPlus.Application.MappingProfile
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Initiative, InitiativeViewModel>();
            CreateMap<User, UserForRegisterViewModel>();
            CreateMap<User, UserForLoginViewModel>()
            .ForMember(dest => dest.RoleName, opts => {
                opts.MapFrom(src => src.UserRole.RoleName);
            });

            CreateMap<UserForRegisterViewModel, User>();
            CreateMap<UserForLoginViewModel, User>();
        }
    }
}
