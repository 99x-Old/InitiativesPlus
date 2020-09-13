using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InitiativesPlus.Application.Interfaces;
using InitiativesPlus.Application.ViewModels;
using InitiativesPlus.Presentation.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace InitiativesPlus.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IStringLocalizer<ErrorStrings> _errorLocalizer;
        public UserController(IAuthService authService, IStringLocalizer<ErrorStrings> errorLocalizer)
        {
            _authService = authService;
            _errorLocalizer = errorLocalizer;
        }

        [HttpPost("register")] //<host>/api/auth/register
        public async Task<IActionResult> Register([FromBody] UserForRegisterViewModel userForRegister)
        {
            // validate request
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            userForRegister.Username = userForRegister.Username.ToLower(); //Convert username to lower case before storing in database.

            if (await _authService.UserExists(userForRegister.Username))
                return BadRequest(_errorLocalizer["UserExists"].Value);

            var createUser = await _authService.Register(userForRegister);

            return StatusCode(201);
        }
    }
}
