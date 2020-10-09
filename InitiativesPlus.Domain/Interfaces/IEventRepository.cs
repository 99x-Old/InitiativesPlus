using System.Collections.Generic;
using System.Threading.Tasks;
using InitiativesPlus.Domain.Models;

namespace InitiativesPlus.Domain.Interfaces
{
    public interface IEventRepository
    {
        Task<Event> CreateEventAsync(Event appEvent);
        Task<List<Event>> QueryEventsAsync();
    }
}
