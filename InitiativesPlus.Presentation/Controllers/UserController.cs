using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InitiativesPlus.Application.Interfaces;
using InitiativesPlus.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InitiativesPlus.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UserController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")] //<host>/api/auth/register
        public async Task<IActionResult> Register([FromBody] UserForRegisterViewModel userForRegister)
        {
            // validate request
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            userForRegister.Username = userForRegister.Username.ToLower(); //Convert username to lower case before storing in database.

            if (await _authService.UserExists(userForRegister.Username))
                return BadRequest("Username is already taken");

            var createUser = await _authService.Register(userForRegister);

            return StatusCode(201);
        }
    }
}
