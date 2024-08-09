using System.ComponentModel.DataAnnotations;

namespace VehicleRegistration.WebAPI.Models
{
    public class ApplicationUser
    {
        public Guid UserId = new Guid();
        [Required(ErrorMessage = "Name can't be blank")]
        public string UserName { get; set; }
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please set the Password Its mandatory")]
        public string Password { get; set; }
    }
}
