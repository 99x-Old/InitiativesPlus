using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InitiativesPlus.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InitiativesPlus.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitiativeController : ControllerBase
    {
        private IInitiativeService _initiativeService;

        public InitiativeController(IInitiativeService initiativeService)
        {
            _initiativeService = initiativeService;
        }

        [HttpGet]
        [Route("GetInitiatives")]
        public async Task<IActionResult> GetInitiatives()
        {
            var initiatives = await _initiativeService.GetInitiatives();
            return Ok(initiatives);
        }
    }
}
