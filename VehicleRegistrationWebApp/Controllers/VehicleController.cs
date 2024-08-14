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

        [HttpGet]
        public IActionResult AddVehicleDetails()
        {
            return View();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddVehicleDetails(VehicleViewModel model)
        {
            string jwtToken = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(jwtToken))
            {
                return RedirectToAction("Login", "Home");
            }
            await _vehicleService.AddVehicles(model, jwtToken);
            return View();
        }

        [HttpGet]
        public IActionResult EditVehicleDetails()
        {
            return View();
        }

        [HttpPut("editVehicle")]
        public async Task<IActionResult> EditVehicleDetails(VehicleViewModel model)
        {
            string jwtToken = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(jwtToken))
            {
                return RedirectToAction("Login", "Home");
            }
            await _vehicleService.UpdateVehicles(model, jwtToken);
            return View();
        }

        [HttpGet]
        public IActionResult DeleteVehicle()
        {
            return View();
        }

        [HttpDelete("deleteVehicle/{vehicleId}")]
        public async Task<IActionResult> DeleteVehicle(Guid vehicleId)
        {
            string jwtToken = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(jwtToken))
            {
                return RedirectToAction("Login", "Home");
            }

            await _vehicleService.DeleteVehicles(vehicleId, jwtToken);
            return View();
        }

        [HttpGet]
        public IActionResult GetVehicleById()
        {
            return View();
        }

        [HttpGet("getVehiclebyId/{vehicleId}")]
        public async Task<IActionResult> GetVehicleById(Guid vehicleId)
        {
            string jwtToken = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(jwtToken))
            {
                return RedirectToAction("Login", "Home");
            }

            var vehicle = await _vehicleService.GetVehicleById(vehicleId, jwtToken);
            return View(vehicle);
        }
    }
}
