using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VehicleRegistrationWebApp.Models;
using VehicleRegistrationWebApp.Services;

namespace VehicleRegistrationWebApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly AccountService _accountService;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(AccountService accountService, IHttpClientFactory httpClientFactory)
        {
            _accountService = accountService;
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpmodel)
        {
            if (!ModelState.IsValid)
            {
                return View(signUpmodel);
            }
            await _accountService.SignUpAsync(signUpmodel);
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _accountService.LoginAsync(model, HttpContext);

            if (!string.IsNullOrEmpty(result.Message))
            {
                if (result.Message.Contains("Logged In Successfully"))
                {
                    ViewBag.Message = result.Message;
                    HttpContext.Session.SetString("Token", result.JwtToken);
                    return RedirectToAction("GetVehiclesDetails", "Vehicle");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View(model); 
                }
            }

            ModelState.AddModelError(string.Empty, "Unexpected error during login.");
            return View(model);
        }

        public IActionResult Logout()
        {
            if (HttpContext.Request.Method == "POST")
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
