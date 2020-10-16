using System.Collections.Generic;
using System.Threading.Tasks;
using InitiativesPlus.Domain.Models;

namespace InitiativesPlus.Domain.Interfaces
{
    public interface IActionRepository
    {
        Task<bool> CreateActionAsync(InitiativeAction initiativeAction);
        Task<List<InitiativeAction>> GetActionsAsync(int id);
        Task<bool> UpdateActionAsync(InitiativeAction initiativeAction);
    }
}
