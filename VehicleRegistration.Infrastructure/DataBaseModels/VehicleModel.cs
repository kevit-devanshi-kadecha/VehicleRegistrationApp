using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// models for mapping with database vehicles table 
namespace VehicleRegistration.Infrastructure.DataBaseModels
{
    public class VehicleModel
    {
        [Key]
        public Guid VehicleId { get; set; }
        [MaxLength(50)]
        public string VehicleNumber { get; set; }
        [MaxLength(150)]
        public string? Description { get; set; }
        [MaxLength(100)]
        public string VehicleOwnerName { get; set; }
        [MaxLength(200)]
        public string? OwnerAddress { get; set; }
        [Phone] 
        [MaxLength(15)]
        public string OwnerContactNumber { get; set; }
        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }
        [MaxLength(50)]
        public string VehicleClass { get; set; }
        [MaxLength(50)]
        public string FuelType { get; set; }
        [ForeignKey("UserId")]
        public UserModel User { get; set; }
        public int UserId { get; set; }
    }
}
