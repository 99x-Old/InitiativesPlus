using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InitiativesPlus.Application.Interfaces;
using InitiativesPlus.Application.ViewModels;
using InitiativesPlus.Presentation.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;

namespace InitiativesPlus.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IStringLocalizer<ErrorStrings> _errorLocalizer;
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        public UserController(IAuthService authService, IStringLocalizer<ErrorStrings> errorLocalizer, IConfiguration config, IUserService userService)
        {
            _authService = authService;
            _errorLocalizer = errorLocalizer;
            _config = config;
            _userService = userService;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginViewModel userForLogin)
        {
            var userFromService = await _authService.Login(userForLogin);
            if (userFromService == null) //User login failed
                return Unauthorized();

            //generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier, userFromService.Id.ToString()),
                    new Claim(ClaimTypes.Name, userFromService.Username),
                    new Claim(ClaimTypes.Role, userFromService.RoleName)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { tokenString });
        }

        [HttpPut("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleViewModel assignRoleViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _authService.UserExists(assignRoleViewModel.UserName))
                return NotFound($"Could not find user with an username of {assignRoleViewModel.UserName}");

            if (await _userService.AssignRoleAsync(assignRoleViewModel))
                return Ok();

            return BadRequest($"Updating role for user {assignRoleViewModel.UserName} failed on save.");
        }

    }
}
