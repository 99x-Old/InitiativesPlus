using AutoMapper;
using InitiativesPlus.Application.Interfaces;
using InitiativesPlus.Application.ViewModels;
using InitiativesPlus.Domain.Interfaces;
using InitiativesPlus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InitiativesPlus.Application.Services
{
    public class AuthService : IAuthService
    {
        public IAuthRepository _authRepository;
        private IMapper _mapper;
        public AuthService(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public async Task<UserForRegisterViewModel> Register(UserForRegisterViewModel userq)
        {
            //var userToCreate = new User
            //{
            //    Username = userq.Username
            //};

            var user = _mapper.Map<User>(userq);
            var createUser = await _authRepository.Register(user, userq.Password);
            var userToReturn = _mapper.Map<UserForRegisterViewModel>(createUser);
            return userToReturn;
            //throw new NotImplementedException();
        }

        public async Task<bool> UserExists(string username)
        {
            return await _authRepository.UserExists(username);
        }
    }
}
