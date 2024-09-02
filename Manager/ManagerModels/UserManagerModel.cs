using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRegistration.Manager.ManagerModels
{
    public class UserManagerModel
    {
        public Guid UserId = new Guid();
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
