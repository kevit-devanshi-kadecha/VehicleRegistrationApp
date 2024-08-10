using VehicleRegistration.Core.DataBaseModels;
using VehicleRegistration.Core.Interfaces;

namespace VehicleRegistration.Infrastructure.Services
{
    public class VehicleService : IVehicleService
    {
        public Task<VehicleModel> GetVehicleByIdAsync(Guid vehicleId)
        {
            throw new NotImplementedException();
        }
        public async Task<VehicleModel> AddVehicle(VehicleModel vehicle)
        {
            throw new NotImplementedException();
        }
        public async Task<VehicleModel> EditVehicle(VehicleModel vehicle)
        {
            throw new NotImplementedException();
        }
        public async Task<VehicleModel> DeleteVehicle(Guid vehicleId)
        {
            throw new NotImplementedException();
        }
    }
}
