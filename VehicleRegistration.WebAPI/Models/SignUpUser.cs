using System;
using System.ComponentModel.DataAnnotations;

namespace VehicleRegistration.WebAPI.Models
{
    public class SignUpUser
    {
        [Required(ErrorMessage = "UserName is mandatory")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email can't ne blank")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
