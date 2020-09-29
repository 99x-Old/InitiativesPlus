using InitiativesPlus.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InitiativesPlus.Application.Interfaces
{
    public interface IInitiativeService
    {
        Task<IEnumerable<InitiativeViewModel>> GetInitiativesAsync();
        Task<InitiativeViewModel> GetInitiativeAsync(int id);
        Task<bool> JoinInitiativeAsync(int id, int userId);
    }
}
