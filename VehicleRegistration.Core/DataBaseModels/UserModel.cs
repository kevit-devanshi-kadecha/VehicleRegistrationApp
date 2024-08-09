using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRegistration.Core.DataBaseModels
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId {  get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string Password { get; set; }
        public ICollection<VehicleModel> Vehicles { get; set; }
    }
}
