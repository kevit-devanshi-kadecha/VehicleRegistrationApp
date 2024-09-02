using Azure.Messaging;
using Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.Core.Interfaces;
using VehicleRegistration.Core.Services;
using VehicleRegistration.Manager;
using VehicleRegistration.Manager.ManagerModels;

namespace VehicleRegistration.WebAPI.Controllers
{
    /// <summary>
    /// Account Controller 
    /// </summary>
    [AllowAnonymous]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IUserService _userService;
        private readonly IJwtService _jwttokenService;
        private readonly ILogger<AccountController> _logger;   
        
        /// <summary>
        /// Controller for User SignUp and Login methods
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="jwtService"></param>
        public AccountController(IUserManager userManager, IUserService userService, IJwtService jwtService, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _userService = userService;
            _jwttokenService = jwtService;
            _logger = logger;
        }
        
        /// <summary>
        /// Method for Registering new User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserManagerModel user)
        {
            _logger.LogInformation("API {controllerName}.{methodName} method", nameof(AccountController), nameof(SignUp));
            try
            {
                if (await _userManager.NewUser(user) == null)
                {
                    return Conflict(new { Message = "Username already exists" });
                }
                else
                {
                    return Ok("Successfully Signed In \nWelcome to Vehicle Registration App");
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "_UserManager : Something went wrong while signing up.");
                throw;
            }
        }

        /// <summary>
        /// Method for User LogIn 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginManagerModel login)
        {
            _logger.LogInformation("API {controllerName}.{methodName} method", nameof(AccountController), nameof(Login));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (Convert.ToBoolean(await _userManager.LoginUser(login)))
                {
                    return Unauthorized("Invalid credentials");
                }
                else
                {
                    var (isAuthenticated, message, jwtToken, tokenExpiration) = await _userManager.LoginUser(login);
                    if (!isAuthenticated)
                    {
                        return Unauthorized(message);
                    }
                    return Ok(new
                    {
                        Message = message,
                        JwtToken = jwtToken,
                        TokenExpiration = tokenExpiration
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "_UserManager : Something went wrong while login up the user.");
                throw;
            }
        }
    }
}

