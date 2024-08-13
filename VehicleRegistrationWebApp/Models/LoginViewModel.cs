using System.ComponentModel.DataAnnotations;

namespace VehicleRegistrationWebApp.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
        public string Message { get; set; }
        public string JwtToken { get; set; }
        public string ExpirationTime { get; set; }
    }
}
