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
        public async Task<IEnumerable<InitiativeViewModel>> GetInitiativesAsync(int? userId)
        {
            var initiatives = await _initiativeRepository.GetInitiativesAsync(userId);
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
            if (!await _initiativeRepository.InitiativeExistsAsync(id))
            {
                return false;
            }
            return await _initiativeRepository.JoinInitiativeAsync(id, userId);

        }

        public async Task<bool> InitiativeExistsAsync(int id)
        {
            return await _initiativeRepository.InitiativeExistsAsync(id);
        }  
        
        public async Task<bool> UserExistsInInitiativeAsync(int id, int userId)
        {
            return await _initiativeRepository.UserExistsInInitiativeAsync(id, userId);
        }        
        
        public async Task<bool> RemoveInitiativeAsync(int id)
        {
            return await _initiativeRepository.RemoveInitiativeAsync(id);
        }
        
        public async Task<bool> RemoveUserFromInitiativeAsync(int id, int userId)
        {
            return await _initiativeRepository.RemoveUserFromInitiativeAsync(id, userId);
        }
    }
}
