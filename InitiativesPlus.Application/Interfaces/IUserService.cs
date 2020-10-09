using System.Collections.Generic;
using System.Threading.Tasks;
using InitiativesPlus.Application.ViewModels;

namespace InitiativesPlus.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> AssignRoleAsync(AssignRoleViewModel assignRoleViewModel);
        Task<List<string>> GetRolesAsync();
        Task<List<string>> GetStatusAsync();
        Task<bool> ChangeStatusAsync(ChangeUserStatusViewModel changeUserStatusViewModel);
        Task<List<string>> GetListOfEmailsAsync();
    }
}
