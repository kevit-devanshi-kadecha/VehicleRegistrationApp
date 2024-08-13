using System.ComponentModel.DataAnnotations;

namespace VehicleRegistration.WebAPI.Models
{
    public class User
    {
        public Guid UserId = new Guid();
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
