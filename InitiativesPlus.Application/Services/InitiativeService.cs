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
        private IInitiativeRepository _initiativeRepository;
        private IMapper _mapper;
        public InitiativeService(IInitiativeRepository initiativeRepository, IMapper mapper)
        {
            _initiativeRepository = initiativeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<InitiativeViewModel>> GetInitiativesAsync()
        {
            var initiatives = await _initiativeRepository.GetInitiativesAsync();
            var initiaviveToReturn = _mapper.Map<IEnumerable<InitiativeViewModel>>(initiatives);
            return initiaviveToReturn;
        }

        public async Task<InitiativeViewModel> GetInitiativeAsync(int id)
        {
            var initiatives = await _initiativeRepository.GetInitiativeAsync(id);
            var initiaviveToReturn = _mapper.Map<InitiativeViewModel>(initiatives);
            return initiaviveToReturn;
        }        
        
        public async Task<bool> JoinInitiativeAsync(int id, int userId)
        {
            return await _initiativeRepository.JoinInitiativeAsync(id, userId);
        }
    }
}
