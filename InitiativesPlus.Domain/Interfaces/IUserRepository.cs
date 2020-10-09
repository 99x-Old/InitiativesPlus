using System.Collections.Generic;
using System.Threading.Tasks;
using InitiativesPlus.Domain.Models;

namespace InitiativesPlus.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AssignRoleAsync(string username, int role);
        Task<List<UserRole>> GetRolesAsync();
        Task<List<User>> GetListOfEmailsAsync();
        Task<List<UserStatus>> GetStatusAsync();
        Task<bool> ChangeStatusAsync(string username, int status);
    }
}
