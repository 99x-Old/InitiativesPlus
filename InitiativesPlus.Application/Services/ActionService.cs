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
        private IActionRepository _actionRepository;
        private IMapper _mapper;
        private readonly IEventRepository _eventRepository;

        public ActionService(IActionRepository actionRepository, IMapper mapper, IEventRepository eventRepository)
        {
            _actionRepository = actionRepository;
            _mapper = mapper;
            _eventRepository = eventRepository;
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
