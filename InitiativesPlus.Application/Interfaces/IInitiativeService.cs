using InitiativesPlus.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InitiativesPlus.Application.Interfaces
{
    public interface IInitiativeService
    {
        Task<IEnumerable<InitiativeViewModel>> GetInitiatives();
    }
}
