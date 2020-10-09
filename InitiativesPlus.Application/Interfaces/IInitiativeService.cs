using InitiativesPlus.Application.ViewModels;
using System.Collections.Generic;
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
        Task<bool> InitiativeExistsAsync(InitiativeForCreate initiativeForCreate);
        Task<bool> RemoveInitiativeAsync(int id);
        Task<bool> RemoveUserFromInitiativeAsync(int id, int userId);
        Task<bool> CreateInitiativeAsync(InitiativeForCreate initiativeForCreate);
        Task<List<EventToReturn>> GetEventsForMonthAsync();
        
    }
}
