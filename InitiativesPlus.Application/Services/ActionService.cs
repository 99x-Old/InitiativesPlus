using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InitiativesPlus.Application.Interfaces;
using InitiativesPlus.Application.ViewModels;
using InitiativesPlus.Domain.Interfaces;
using InitiativesPlus.Domain.Models;

namespace InitiativesPlus.Application.Services
{
    public class ActionService : IActionService
    {
        private readonly IActionRepository _actionRepository;
        private readonly IMapper _mapper;

        public ActionService(IActionRepository actionRepository, IMapper mapper)
        {
            _actionRepository = actionRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateActionAsync(ActionToCreate initiativeForCreate)
        {
            var action = _mapper.Map<InitiativeAction>(initiativeForCreate);
            return await _actionRepository.CreateActionAsync(action);
        }

        public async Task<List<ActionToReturn>> GetActionsAsync(int id)
        {
            var actions = await _actionRepository.GetActionsAsync(id);
            if (actions == null)
                return null;
            var actionsToReturn = _mapper.Map<List<ActionToReturn>>(actions);
            return actionsToReturn;
        }

        public async Task<bool> UpdateActionAsync(ActionToUpdate actionToUpdate)
        {
            var action = _mapper.Map<InitiativeAction>(actionToUpdate);
            return await _actionRepository.UpdateActionAsync(action);
        }
    }
}
