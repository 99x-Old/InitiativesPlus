using AutoMapper;
using InitiativesPlus.Application.Interfaces;
using InitiativesPlus.Application.ViewModels;
using InitiativesPlus.Domain.Interfaces;
using InitiativesPlus.Domain.Models;
using System;
using System.Threading.Tasks;

namespace InitiativesPlus.Application.Services
{
    public class AuthService : IAuthService
    {
        private IAuthRepository _authRepository;
        private IMapper _mapper;
        public AuthService(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public async Task<UserForLoginViewModel> Login(UserForLoginViewModel user)
        {
            var loginUser = await _authRepository.Login(user.Username.ToLower(), user.Password);
            return _mapper.Map<UserForLoginViewModel>(loginUser);
        }

        public async Task<UserForRegisterViewModel> Register(UserForRegisterViewModel user)
        {
            var registerUser = _mapper.Map<User>(user);
            var createUser = await _authRepository.Register(registerUser, user.Password);
            var userToReturn = _mapper.Map<UserForRegisterViewModel>(createUser);
            return userToReturn;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _authRepository.UserExists(username);
        }
    }
}
