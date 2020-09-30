using InitiativesPlus.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InitiativesPlus.Application.Interfaces
{
    public interface IInitiativeService
    {
        Task<IEnumerable<InitiativeViewModel>> GetInitiativesAsync(int? userId);
        Task<InitiativeViewModel> GetInitiativeAsync(int id);
        Task<bool> JoinInitiativeAsync(int id, int userId);
        Task<bool> UserExistsInInitiativeAsync(int id, int userId);
        Task<bool> InitiativeExistsAsync(int id);
        Task<bool> RemoveInitiativeAsync(int id);
        Task<bool> RemoveUserFromInitiativeAsync(int id, int userId);
    }
}
