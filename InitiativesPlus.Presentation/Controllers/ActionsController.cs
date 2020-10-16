using System.Threading.Tasks;
using InitiativesPlus.Application.Interfaces;
using InitiativesPlus.Application.ViewModels;
using InitiativesPlus.Infrastructure.Data.StaticClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InitiativesPlus.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionsController : ControllerBase
    {
        private readonly IActionService _actionService;

        public ActionsController(IActionService actionService)
        {
            _actionService = actionService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = RoleTypes.InitiativeLead)]
        public async Task<IActionResult> CreateAction(ActionToCreate actionToCreate)
        {
            bool success = await _actionService.CreateActionAsync(actionToCreate);
            return Ok(success);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = RoleTypes.InitiativeLead)]
        public async Task<IActionResult> GetInitiativeActions(int id)
        {
            var actions = await _actionService.GetActionsAsync(id);
            return Ok(actions);
        }

        [HttpPut]
        [Authorize(Roles = RoleTypes.InitiativeLead)]
        public async Task<IActionResult> UpdateAction(ActionToUpdate actionToUpdate)
        {
            var actions = await _actionService.UpdateActionAsync(actionToUpdate);
            return Ok();
        }
    }
}
