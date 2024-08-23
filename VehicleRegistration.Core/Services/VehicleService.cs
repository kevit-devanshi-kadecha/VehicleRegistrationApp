using Microsoft.EntityFrameworkCore;
using VehicleRegistration.Infrastructure.DataBaseModels;
using VehicleRegistration.Core.Interfaces;
using VehicleRegistration.Infrastructure;

namespace VehicleRegistration.Core.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        public VehicleService(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public async Task<List<VehicleModel>> GetVehicleDetails(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId), "UserId cannot be null.");
            }

            List<VehicleModel> vehicleDetails = _context.VehiclesDetails.Where(v => v.UserId == int.Parse(userId)).ToList()!;
            return vehicleDetails;
        }

        public Task<VehicleModel> GetVehicleByIdAsync(Guid vehicleId)
        {
            return _context.VehiclesDetails.FindAsync(vehicleId).AsTask()!;
        }

        public async Task<VehicleModel> AddVehicle(VehicleModel newVehicle)
        {
            _context.VehiclesDetails.Add(newVehicle);
            await _context.SaveChangesAsync();
            return newVehicle;
        }

        public async Task<VehicleModel> EditVehicle(VehicleModel vehicle, string userId)
        {
            var existingVehicle = await _context.VehiclesDetails.FindAsync(vehicle.VehicleId);
            if (existingVehicle == null)
            {
                throw new NullReferenceException("Vehicle not found.");
            }
            var hasChanges = typeof(VehicleModel).GetProperties()
                .Any(prop => prop.GetValue(existingVehicle)?.ToString() != prop.GetValue(vehicle)?.ToString());

            if (!hasChanges)
            {
                return null;
            }

            // Update the existing vehicle details with new values 
            foreach (var prop in typeof(VehicleModel).GetProperties())
            {
                var newValue = prop.GetValue(vehicle);
                prop.SetValue(existingVehicle, newValue);
            }

            _context.VehiclesDetails.Update(existingVehicle);
            await _context.SaveChangesAsync();

            return existingVehicle;
        }

        public async Task<VehicleModel> DeleteVehicle(Guid vehicleId)
        {
            var vehicle = await _context.VehiclesDetails.FindAsync(vehicleId);
            if (vehicle == null)
                return null;

            _context.VehiclesDetails.Remove(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }
    }
}
