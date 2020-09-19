using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InitiativesPlus.Application.ViewModels;

namespace InitiativesPlus.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> AssignRoleAsync(AssignRoleViewModel assignRoleViewModel);
    }
}
