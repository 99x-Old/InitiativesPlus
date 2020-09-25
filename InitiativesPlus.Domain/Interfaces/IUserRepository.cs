using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InitiativesPlus.Domain.Models;

namespace InitiativesPlus.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AssignRoleAsync(string username, int role);
        Task<List<UserRole>> GetRolesAsync();
    }
}
