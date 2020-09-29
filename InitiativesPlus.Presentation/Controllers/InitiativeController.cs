using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using InitiativesPlus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InitiativesPlus.Presentation.Controllers
{
    //[Authorize(Roles = "Initiative Lead")]
    [Authorize(Roles = "User")]
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
        [Route("GetInitiatives")]
        public async Task<IActionResult> GetInitiatives()
        {
            var initiatives = await _initiativeService.GetInitiativesAsync();
            return Ok(initiatives);
        }

        [HttpGet]
        [Route("GetInitiatives/{id}")]
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
        [Route("Join/{id}")]
        public async Task<IActionResult> JoinInitiative(int id)
        {
            int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _initiativeService.JoinInitiativeAsync(id, userId);
            return Ok();
        }
    }
}
