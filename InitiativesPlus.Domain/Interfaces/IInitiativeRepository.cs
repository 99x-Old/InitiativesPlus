
using InitiativesPlus.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InitiativesPlus.Domain.Interfaces
{
    public interface IInitiativeRepository
    {
        Task<List<Initiative>> GetInitiativesAsync(int? userId);
        Task<Initiative> GetInitiativeAsync(int id);
        Task<bool> JoinInitiativeAsync(int id, int userId);
        Task<bool> UserExistsInInitiativeAsync(int id, int userId);
        Task<bool> RemoveInitiativeAsync(int id);
        Task<bool> RemoveUserFromInitiativeAsync(int id, int userId);
        Task<bool> CreateInitiativeAsync(Initiative initiative, string year);
        Task<bool> InitiativeExistsAsync(int id);
        Task<bool> InitiativeExistsAsync(string initiative, string year);
        Task<List<User>> InitiativeUsers(int id);
    }
}
