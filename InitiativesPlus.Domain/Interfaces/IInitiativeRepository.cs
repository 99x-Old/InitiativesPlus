
using InitiativesPlus.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InitiativesPlus.Domain.Interfaces
{
    public interface IInitiativeRepository
    {
        Task<List<Initiative>> GetInitiativesAsync();
        Task<Initiative> GetInitiativeAsync(int id);
        Task<bool> JoinInitiativeAsync(int id, int userId);
    }
}
