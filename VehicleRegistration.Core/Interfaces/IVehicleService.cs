using VehicleRegistration.Infrastructure.DataBaseModels;

namespace VehicleRegistration.Core.Interfaces
{
    public interface IVehicleService
    {
        Task<List<VehicleModel>> GetVehicleDetails(string userId);
        Task<VehicleModel> GetVehicleByIdAsync(Guid vehicleId);
        Task<VehicleModel> AddVehicle(VehicleModel vehicle);
        Task<VehicleModel> EditVehicle(VehicleModel vehicle, string userId);
        Task<VehicleModel> DeleteVehicle(Guid vehicleId);
    }
}
