using Azure.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.Core.Interfaces;
using VehicleRegistration.Infrastructure.DataBaseModels;
using VehicleRegistration.WebAPI.Models;

namespace VehicleRegistration.WebAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwttokenService;

        public AccountController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwttokenService = jwtService;
        }
        
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if the username already exists
            var existingUser = await _userService.GetUserByNameAsync(user.UserName);
            if (existingUser != null)
                return Conflict(new { Message = "Username already exists" });

            // Create a new user
            var newUser = new UserModel
            {
                UserName = user.UserName,
                UserEmail = user.Email
            };

            await _userService.AddUser(newUser, user.Password);
            return Ok("Successfully Signed In \nWelcome to Vehicle Registration App");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isAuthenticated = await _userService.AuthenticateUser(login.UserName, login.Password);

            if (!isAuthenticated)
                return Unauthorized("Invalid credentials");

            var user = await _userService.GetUserByNameAsync(login.UserName);
            var tokenResponse = _jwttokenService.CreateJwtToken(user);

            return Ok(new
            {
                Message = "Logged In Successfully",
                JwtToken = tokenResponse.Token,
                TokenExpiration = tokenResponse.Expiration
            });
        }
    }
}

