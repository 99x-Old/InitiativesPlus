using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InitiativesPlus.Application.Interfaces;
using InitiativesPlus.Application.ViewModels;
using InitiativesPlus.Domain.Interfaces;

namespace InitiativesPlus.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<bool> AssignRoleAsync(AssignRoleViewModel assignRoleViewModel)
        {
            return await _userRepository.AssignRoleAsync(assignRoleViewModel.UserName, assignRoleViewModel.NewRole);
        }
    }
}
