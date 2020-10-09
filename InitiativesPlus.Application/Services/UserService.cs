using System.Collections.Generic;
using System.Linq;
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
        
        public async Task<bool> ChangeStatusAsync(ChangeUserStatusViewModel changeUserStatusViewModel)
        {
            return await _userRepository.ChangeStatusAsync(changeUserStatusViewModel.UserName, changeUserStatusViewModel.NewStatus);
        }

        public async Task<List<string>> GetListOfEmailsAsync()
        {
            var users = await _userRepository.GetListOfEmailsAsync();
            return users.Where(x => x.Email != "").Select(x => x.Email).ToList();
        }

        public async Task<List<string>> GetRolesAsync()
        {
            var users = await _userRepository.GetRolesAsync();
            return users.Select(x => x.RoleName).ToList();
        }

        public async Task<List<string>> GetStatusAsync()
        {
            var statuses = await _userRepository.GetStatusAsync();
            return statuses.Select(x => x.Status).ToList();
        }
    }
}
