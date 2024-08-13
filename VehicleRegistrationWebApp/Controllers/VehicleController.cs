using Microsoft.AspNetCore.Mvc;
using VehicleRegistrationWebApp.Models;
using VehicleRegistrationWebApp.Services;

namespace VehicleRegistrationWebApp.Controllers
{
    //for accessing the API 
    [Route("api/[controller]")]
    public class VehicleController : Controller
    {
        private readonly VehicleService _vehicleService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        public VehicleController(IHttpClientFactory httpClientFactory, VehicleService vehicleService, ILogger logger)
        {
            _httpClientFactory = httpClientFactory;
            _vehicleService = vehicleService;
            _logger = logger;
        }
        [HttpGet("getVehicles")]
        public async Task<IActionResult> GetVehiclesDetails()
        {
            var vehicles = await _vehicleService.GetVehicles();
            return View(vehicles);
        }

        [HttpPost]
        public IActionResult AddVehicleDetails()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVehicleDetails()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteVehicle()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicleById()
        {
            return View();
        }
    }
}
