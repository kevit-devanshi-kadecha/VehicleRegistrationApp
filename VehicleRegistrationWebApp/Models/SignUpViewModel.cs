using System.ComponentModel.DataAnnotations;

namespace VehicleRegistrationWebApp.Models
{
    public class SignUpViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
