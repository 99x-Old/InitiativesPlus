using AutoMapper;
using InitiativesPlus.Application.ViewModels;
using InitiativesPlus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InitiativesPlus.Application.MappingProfile
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Initiative, InitiativeViewModel>();
        }
    }
}
