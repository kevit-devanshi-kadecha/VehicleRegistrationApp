using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleRegistrationWebApp.Models
{
    public class VehicleViewModel
    {
        public Guid VehicleId { get; set; } 

        [DisplayName("Vehicle Number")]
        [Required(ErrorMessage = "RTO register vehicle number is necessary")]
        public string VehicleNumber { get; set; }

        [DisplayName("Description")]
        public string? Description { get; set; }

        [DisplayName("Vehicle OwnerName")]
        [Required(ErrorMessage = "Please provide owner name")]
        public string VehicleOwnerName { get; set; }

        [DisplayName("Address")]
        public string? OwnerAddress { get; set; }

        [DisplayName("Owner Contact Number")]
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number should contain digits only")]
        public string OwnerContactNumber { get; set; }

        [DisplayName("Email")]
        public string? Email { get; set; }

        [DisplayName("Vehicle Class/Type")]
        [Required(ErrorMessage = "Please provide the class/type of the vehicle")]
        public string VehicleClass { get; set; }

        [DisplayName("Fuel Type")]
        [Required(ErrorMessage = "Provide the type of fuel the vehicle consumes")]
        public string FuelType { get; set; }

        public int UserId { get; set; }
    }
}
