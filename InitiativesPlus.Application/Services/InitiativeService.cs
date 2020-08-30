using InitiativesPlus.Application.Interfaces;
using InitiativesPlus.Application.ViewModels;
using InitiativesPlus.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InitiativesPlus.Application.Services
{
    public class InitiativeService : IInitiativeService
    {
        public IInitiativeRepository _initiativeRepository;
        public InitiativeService(IInitiativeRepository initiativeRepository)
        {
            _initiativeRepository = initiativeRepository;
        }
        public async Task<InitiativeViewModel> GetInitiatives()
        {

            return new InitiativeViewModel()
            {
                Initiatives = await _initiativeRepository.GetInitiatives()
            };
        }
    }
}
