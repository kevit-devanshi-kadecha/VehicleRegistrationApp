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
            List<VehicleModel> vehicleDetails = _context.VehiclesDetails.Where( v => v.UserId == int.Parse(userId)).ToList()!;
            return vehicleDetails;
        }

        public Task<VehicleModel> GetVehicleByIdAsync(Guid vehicleId)
        {
            return _context.VehiclesDetails.FindAsync(vehicleId).AsTask();
        }

        public async Task<VehicleModel> AddVehicle(VehicleModel newVehicle)
        {
            _context.VehiclesDetails.Add(newVehicle);
            await _context.SaveChangesAsync();
            return newVehicle;
        }

        public async Task<VehicleModel> EditVehicle(VehicleModel vehicle)
        {
            _context.VehiclesDetails.Update(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
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
