using VehicleRegistration.Core.DataBaseModels;

namespace VehicleRegistration.Core.Interfaces
{
    public interface IVehicleService
    {
        Task<VehicleModel> GetVehicleByIdAsync(Guid vehicleId);
        Task<VehicleModel> AddVehicle(VehicleModel vehicle);
        Task<VehicleModel> EditVehicle(VehicleModel vehicle);
        Task<VehicleModel> DeleteVehicle(Guid vehicleId);
    }
}
