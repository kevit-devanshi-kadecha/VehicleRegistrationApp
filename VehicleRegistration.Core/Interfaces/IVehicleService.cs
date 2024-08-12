using VehicleRegistration.Infrastructure.DataBaseModels;

namespace VehicleRegistration.Core.Interfaces
{
    public interface IVehicleService
    {
        Task<VehicleModel> GetVehicleByIdAsync(Guid vehicleId);
        //List<VehicleModel> GetVehicleDetails();
        //Task<VehicleModel> AddVehicle(VehicleModel vehicle, string username);
        Task<VehicleModel> AddVehicle(VehicleModel vehicle);
        Task<VehicleModel> EditVehicle(VehicleModel vehicle);
        Task<VehicleModel> DeleteVehicle(Guid vehicleId);
    }
}
