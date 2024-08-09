using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebAPI.Models;

namespace VehicleRegistration.WebAPI.Controllers
{
    [AllowAnonymous]
    public class AccountController : CustomControllerBase
    {
        //private readonly IJwtService _jwtService;
        //public AccountController(IJwtService jwtService) 
        //{
        //    _jwtService = jwtService;
        //}
        

        //[HttpGet]
        //public async Task<ActionResult<ApplicationUser> SignUp(SignUpUser signUpUser)
        //{
        //    //Create User
        //    ApplicationUser user = new ApplicationUser()
        //    {
        //        UserId = Guid.NewGuid(),
        //        UserName = signUpUser.UserName
        //    }
        // }

    }
}

