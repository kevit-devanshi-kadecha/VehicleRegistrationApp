using System;
using System.ComponentModel.DataAnnotations;

namespace VehicleRegistration.Core.DataBaseModels
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
