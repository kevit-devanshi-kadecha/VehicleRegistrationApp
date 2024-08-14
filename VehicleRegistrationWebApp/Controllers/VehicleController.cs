using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using VehicleRegistrationWebApp.Models;
using VehicleRegistrationWebApp.Services;

namespace VehicleRegistrationWebApp.Controllers
{
    public class VehicleController : Controller
    {
        private readonly VehicleService _vehicleService;
        private readonly IHttpClientFactory _httpClientFactory;
        public VehicleController(IHttpClientFactory httpClientFactory, VehicleService vehicleService)
        {
            _httpClientFactory = httpClientFactory;
            _vehicleService = vehicleService;
        }
        [HttpGet("getVehicles")]
        public async Task<IActionResult> GetVehiclesDetails()
        {
            string jwtToken = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(jwtToken) ) 
            {
               return RedirectToAction("Login", "Home");
            }

            var vehicles = await _vehicleService.GetVehicles(jwtToken);
            return View(vehicles);
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicleDetails()
        {
            string jwtToken = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(jwtToken))
            {
                return RedirectToAction("Login", "Home");
            }
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVehicleDetails()
        {
            string jwtToken = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(jwtToken))
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult DeleteVehicle()
        {
            string jwtToken = HttpContext.Session.GetString("Token");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicleById()
        {
            string jwtToken = HttpContext.Session.GetString("Token");
            return View();
        }
    }
}
