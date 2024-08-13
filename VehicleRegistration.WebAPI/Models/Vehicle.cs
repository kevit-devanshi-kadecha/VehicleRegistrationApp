using System.ComponentModel.DataAnnotations;

// model for adding the vehicle details when client calls the Add method 
namespace VehicleRegistration.WebAPI.Models
{
    public class Vehicle
    {
        public Guid VehicleId { get; set; } 
        public string VehicleNumber { get; set; }
        public string? Description { get; set; }
        public string VehicleOwnerName { get; set; }
        public string? OwnerAddress { get; set; }
        public string OwnerContactNumber { get; set; }
        public string? Email { get; set; }
        public string VehicleClass { get; set; }
        public string FuelType { get; set; }
    }
}
