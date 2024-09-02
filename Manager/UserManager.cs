using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VehicleRegistration.Core.Interfaces;
using VehicleRegistration.Core.Services;
using VehicleRegistration.Infrastructure.DataBaseModels;
using VehicleRegistration.Manager;
using VehicleRegistration.Manager.ManagerModels;

namespace Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly ILogger<UserManager> _logger;
        public UserManager(IUserService userService, IJwtService jwtService ,ILogger<UserManager> logger,) 
        {
            _userService = userService;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<(bool isAuthenticated, string message, string jwtToken, DateTime tokenExpiration)> LoginUser(LoginManagerModel login)
        {
            var isAuthenticated = await _userService.AuthenticateUser(login.UserName, login.Password);

            if (!isAuthenticated)
                return (false, "Invalid credentials", string.Empty, DateTime.MinValue);

            var user = await _userService.GetUserByNameAsync(login.UserName);
            var tokenResponse = _jwtService.CreateJwtToken(user);

            return (true, "Logged In Successfully", tokenResponse.Token, tokenResponse.Expiration);
        }

        public async Task<UserManagerModel> NewUser(UserManagerModel user)
        {
            var newUser = new UserModel
            {
                UserName = user.UserName,
                UserEmail = user.Email,
            };
            await _userService.AddUser(newUser, user.Password);
            return user;
        }

        
    }
}
