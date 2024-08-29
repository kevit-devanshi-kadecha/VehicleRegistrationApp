﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.Infrastructure.DataBaseModels;
using VehicleRegistration.Core.Interfaces;
using VehicleRegistration.WebAPI.Models;
using System.Net.Http.Headers;
using System.Security.Claims;
using VehicleRegistration.Infrastructure;
using Microsoft.Data.SqlClient;

namespace VehicleRegistration.WebAPI.Controllers
{
    [Authorize] 
    [Route("api/Vehicle")]
    [ApiController]

    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicleService"></param>
        /// <param name="context"></param>
        public VehicleController(IVehicleService vehicleService, ApplicationDbContext context)
        {
            _vehicleService = vehicleService; 
            _context = context; 
        }

        /// <summary>
        /// Method For Vehicles Details
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAllVehicles")]
        public async Task<IActionResult> GetAllVehicles()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var vehicles = await _vehicleService.GetVehicleDetails(userId);
            return Ok(vehicles);
        }

        /// <summary>
        /// Method For adding new vehicle
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [ProducesResponseType(201)] // Created 
        [ProducesResponseType(403)]
        public async Task<IActionResult> AddNewVehicle([FromBody] Vehicle vehicle)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newVehicle = new VehicleModel
            {
                VehicleId = new Guid(), 
                VehicleNumber = vehicle.VehicleNumber,
                Description = vehicle.Description,
                VehicleOwnerName = vehicle.VehicleOwnerName,
                OwnerAddress = vehicle.OwnerAddress,
                OwnerContactNumber = vehicle.OwnerContactNumber,
                Email = vehicle.Email,
                VehicleClass = vehicle.VehicleClass,
                FuelType = vehicle.FuelType,
                UserId = int.Parse(userId!)
            };
            try
            {
                await _vehicleService.AddVehicle(newVehicle);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while saving vehicle to the database.", ex);
            }
            return Ok("Vehicle Added Successfully");
        }

        /// <summary>
        /// Method for editing Vehicle Details
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        [HttpPut("edit")]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> EditVehicle([FromBody] Vehicle vehicle)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicleModel = new VehicleModel
            {
                VehicleId = vehicle.VehicleId,
                VehicleNumber = vehicle.VehicleNumber,
                Description = vehicle.Description,
                VehicleOwnerName = vehicle.VehicleOwnerName,
                OwnerAddress = vehicle.OwnerAddress,
                OwnerContactNumber = vehicle.OwnerContactNumber,
                Email = vehicle.Email,
                VehicleClass = vehicle.VehicleClass,
                FuelType = vehicle.FuelType,
                UserId = int.Parse(userId) 
            };
            var updatedVehicle = await _vehicleService.EditVehicle(vehicleModel , userId);

            if (updatedVehicle == null)
            {
                return Ok("No modifications applied. The vehicle details are already up-to-date.");
            }

            return Ok("Vehicle Details Edited Successfully");
        }

        /// <summary>
        /// Method for deleting Vehicle
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(401)]
        public async Task<IActionResult> DeleteVehicle(Guid id)
        {
            var existingVehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (existingVehicle == null)
                return NotFound();

            await _vehicleService.DeleteVehicle(id);
            return Ok("Vehicle Deleted successfully"); 
        }

        /// <summary>
        /// Method for get vehicle by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetVehicleById(Guid id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }
    }
}
