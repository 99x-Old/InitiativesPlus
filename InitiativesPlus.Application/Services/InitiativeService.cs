using AutoMapper;
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
        private IMapper _mapper;
        public InitiativeService(IInitiativeRepository initiativeRepository, IMapper mapper)
        {
            _initiativeRepository = initiativeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<InitiativeViewModel>> GetInitiatives()
        {
            var initiatives = await _initiativeRepository.GetInitiatives();
            var initiaviveToReturn = _mapper.Map<IEnumerable<InitiativeViewModel>>(initiatives);
            return initiaviveToReturn;
        }
    }
}
