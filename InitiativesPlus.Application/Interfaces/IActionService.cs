using System.Collections.Generic;
using System.Threading.Tasks;
using InitiativesPlus.Application.ViewModels;

namespace InitiativesPlus.Application.Interfaces
{
    public interface IActionService
    {
        Task<bool> CreateActionAsync(ActionToCreate initiativeForCreate);
        Task<List<ActionToReturn>> GetActionsAsync(int id);
        Task<bool> UpdateActionAsync(ActionToUpdate actionToUpdate);
    }
}
