
using InitiativesPlus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InitiativesPlus.Domain.Interfaces
{
    public interface IInitiativeRepository
    {
        Task<List<Initiative>> GetInitiatives();
    }
}
