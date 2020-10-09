using System;
using AutoMapper;
using InitiativesPlus.Application.Interfaces;
using InitiativesPlus.Application.ViewModels;
using InitiativesPlus.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using InitiativesPlus.Domain.Models;

namespace InitiativesPlus.Application.Services
{
    public class InitiativeService : IInitiativeService
    {
        private IInitiativeRepository _initiativeRepository;
        private IMapper _mapper;
        private readonly IEventRepository _eventRepository;

        public InitiativeService(IInitiativeRepository initiativeRepository, IMapper mapper, IEventRepository eventRepository)
        {
            _initiativeRepository = initiativeRepository;
            _mapper = mapper;
            _eventRepository = eventRepository;
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

        public async Task<bool> InitiativeExistsAsync(InitiativeForCreate initiativeForCreate)
        {
            return await _initiativeRepository.InitiativeExistsAsync(initiativeForCreate.Name, initiativeForCreate.Year);
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

        public async Task<bool> CreateInitiativeAsync(InitiativeForCreate initiativeForCreate)
        {
            var initiaviveToCreate = _mapper.Map<Initiative>(initiativeForCreate);
            bool success =  await _initiativeRepository.CreateInitiativeAsync(initiaviveToCreate, initiativeForCreate.Year);

            if (success)
            {
                Event appEvent = new Event
                {
                    Id = Guid.NewGuid().ToString(),
                    Month = "09-2020",
                    Initiative = initiativeForCreate.Name,
                    Type = "New",
                    Value = 0,
                    InitiativeYear = initiativeForCreate.Year
                };
                await _eventRepository.CreateEventAsync(appEvent);
                return true;
            }

            return false;
        }

        public async Task<List<EventToReturn>> GetEventsForMonthAsync()
        {
            var events = await _eventRepository.QueryEventsAsync();
            var eventToReturn = _mapper.Map<List<EventToReturn>>(events);
            return eventToReturn;
        }
    }
}
