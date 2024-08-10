using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.Core.DataBaseModels;
using VehicleRegistration.Core.Interfaces;
using VehicleRegistration.WebAPI.Models;

namespace VehicleRegistration.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Ensures the user is authenticated
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        // show vehicle method list vehicles 


        [HttpPost("add")]
        public async Task<IActionResult> AddNewVehicle([FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newVehicle = new VehicleModel
            {
                VehicleId = Guid.NewGuid(), // Auto-generate the VehicleId
                VehicleNumber = vehicle.VehicleNumber,
                Description = vehicle.Description,
                VehicleOwnerName = vehicle.VehicleOwnerName,
                OwnerAddress = vehicle.OwnerAddress,
                OwnerContactNumber = vehicle.OwnerContactNumber,
                Email = vehicle.Email,
                VehicleClass = vehicle.VehicleClass,
                FuelType = vehicle.FuelType,
                // Assume the User information is set based on authentication context
            };

            var addedVehicle = await _vehicleService.AddVehicle(newVehicle);
            return Ok(addedVehicle);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditVehicle([FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingVehicle = await _vehicleService.GetVehicleByIdAsync(vehicle.VehicleId);
            if (existingVehicle == null)
                return NotFound();

            existingVehicle.VehicleNumber = vehicle.VehicleNumber;
            existingVehicle.Description = vehicle.Description;
            existingVehicle.VehicleOwnerName = vehicle.VehicleOwnerName;
            existingVehicle.OwnerAddress = vehicle.OwnerAddress;
            existingVehicle.OwnerContactNumber = vehicle.OwnerContactNumber;
            existingVehicle.Email = vehicle.Email;
            existingVehicle.VehicleClass = vehicle.VehicleClass;
            existingVehicle.FuelType = vehicle.FuelType;

            var updatedVehicle = await _vehicleService.EditVehicle(existingVehicle);

            return Ok(updatedVehicle);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteVehicle(Guid id)
        {
            var existingVehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (existingVehicle == null)
                return NotFound();

            await _vehicleService.DeleteVehicle(id);

            return NoContent(); // Successfully deleted
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetVehicleById(Guid id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }
    }
}
