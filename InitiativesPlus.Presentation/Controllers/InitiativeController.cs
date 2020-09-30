using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using InitiativesPlus.Application.Interfaces;
using InitiativesPlus.Application.ViewModels;
using InitiativesPlus.Infrastructure.Data.StaticClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InitiativesPlus.Presentation.Controllers
{
    //[Authorize(Roles = "Initiative Lead")]
    [Authorize(Roles = RoleTypes.User)]
    [Route("api/[controller]")]
    [ApiController]
    public class InitiativeController : ControllerBase
    {
        private readonly IInitiativeService _initiativeService;

        public InitiativeController(IInitiativeService initiativeService)
        {
            _initiativeService = initiativeService;
        }

        [HttpGet]
        [Route("getinitiatives/{filter?}")]
        public async Task<IActionResult> GetInitiatives([FromQuery] string filter)
        {
            IEnumerable<InitiativeViewModel> initiatives = null;
            if (filter == "my")
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                initiatives = await _initiativeService.GetInitiativesAsync(userId);
            }
            else
            {
                initiatives = await _initiativeService.GetInitiativesAsync(null);
            }
            return Ok(initiatives);
        }

        [HttpGet]
        [Route("getinitiative/{id}")]
        public async Task<IActionResult> GetInitiative(int id)
        {
            var initiative = await _initiativeService.GetInitiativeAsync(id);
            if (initiative == null)
            {
                return NotFound();
            }
            return Ok(initiative);
        }

        [HttpGet]
        [Route("join/{id}")]
        public async Task<IActionResult> JoinInitiative(int id)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (await _initiativeService.UserExistsInInitiativeAsync(id, userId))
            {
                return BadRequest("You're already a member of this initiative");
            }
            await _initiativeService.JoinInitiativeAsync(id, userId);
            return Ok();
        }

        [HttpGet]
        [Route("exists/{id}")]
        public async Task<IActionResult> InitiativeExists(int id)
        {
            bool success = await _initiativeService.InitiativeExistsAsync(id);
            return Ok(success);
        }

        [HttpDelete]
        [Route("remove/{id}")]
        public async Task<IActionResult> RemoveInitiative(int id)
        {
            bool success = await _initiativeService.RemoveInitiativeAsync(id);
            return Ok(success);
        }

        [HttpDelete]
        [Route("remove-user/{id}/{userId}")]
        public async Task<IActionResult> RemoveUser(int id, int userId)
        {
            bool success = await _initiativeService.RemoveUserFromInitiativeAsync(id, userId);
            return Ok(success);
        }

        [HttpDelete]
        [Route("remove-me/{id}")]
        public async Task<IActionResult> RemoveUser(int id)
        {
            int userToRemove = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!await _initiativeService.UserExistsInInitiativeAsync(id, userToRemove))
            {
                return BadRequest("You're not a member of this initiative.");
            }
            bool success = await _initiativeService.RemoveUserFromInitiativeAsync(id, userToRemove);
            return Ok(success);
        }
    }
}
