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
        public IActionResult Addvehicledetails()
        {
            string jwtToken = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(jwtToken))
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicleDetails(VehicleViewModel model)
        {
            string jwtToken = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(jwtToken))
            {
                return RedirectToAction("Login", "Home");
            }

            await _vehicleService.AddVehicles(model, jwtToken);
            return RedirectToAction("GetVehiclesDetails");
        }

        [HttpGet("editVehicle/{vehicleId}")]
        public async Task<IActionResult> EditVehicleDetails([FromRoute] Guid vehicleId)
        {
            string jwtToken = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(jwtToken))
            {
                return RedirectToAction("Login", "Home");
            }

            var vehicle = await _vehicleService.GetVehicleByIdAsync(vehicleId, jwtToken);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        [HttpPost("editVehicle/{vehicleId}")]
        public async Task<IActionResult> EditVehicleDetails([FromForm] VehicleViewModel model)
        {
            
            string jwtToken = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(jwtToken))
            {
                return RedirectToAction("Login", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(model); 
            }

            var result = await _vehicleService.UpdateVehicles(model, jwtToken);
            if (result == "Vehicle Details Edited Successfully")
            {
                return RedirectToAction("GetVehiclesDetails");
            }
            else if (result == "No modifications applied")
            {
                ModelState.AddModelError("", "No changes were made to the vehicle details.");
                return View(model); 
            }
            else
            {
                ModelState.AddModelError("", "Failed to update vehicle details.");
                return View(model);
            }
        }

        [HttpGet("deleteVehicle/{vehicleId}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute] Guid vehicleId)
        {
            string jwtToken = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(jwtToken))
            {
                return RedirectToAction("Login", "Home");
            }

            var vehicle = await _vehicleService.GetVehicleByIdAsync(vehicleId, jwtToken);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View("DeleteVehicle",vehicle);
        }

        [HttpDelete("deleteVehicle")]
        public async Task<IActionResult> PostDeleteVehicle([FromRoute] Guid vehicleId)
        {
            string jwtToken = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(jwtToken))
            {
                return RedirectToAction("Login", "Home");
            }

            await _vehicleService.DeleteVehicles(vehicleId, jwtToken);
            return RedirectToAction("GetVehiclesDetails", "Vehicle");
        }


        [HttpGet]
        public IActionResult GetVehicleById()
        {
            return View();
        }

        [HttpGet("getVehiclebyId")]
        public async Task<IActionResult> GetVehicleById([FromQuery] VehicleRequest request)
        {
            string jwtToken = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(jwtToken))
            {
                return RedirectToAction("Login", "Home");
            }

            var vehicle = await _vehicleService.GetVehicleByIdAsync(request.VehicleId, jwtToken);
             if (vehicle == null)
             {
                 return NotFound();
             }
            return View(vehicle);
        }
    }
}
