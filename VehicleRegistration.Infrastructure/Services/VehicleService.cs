using Microsoft.EntityFrameworkCore;
using VehicleRegistration.Core.DataBaseModels;
using VehicleRegistration.Core.Interfaces;

namespace VehicleRegistration.Infrastructure.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _context;

        public VehicleService(ApplicationDbContext context)
        {
            _context = context;
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
            if (vehicle == null) return null;

            _context.VehiclesDetails.Remove(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }
    }
}
