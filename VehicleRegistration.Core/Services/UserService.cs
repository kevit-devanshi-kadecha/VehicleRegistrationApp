using VehicleRegistration.Core.Interfaces;
using VehicleRegistration.Core.DataBaseModels;
using VehicleRegistration.Core.Services;
using System.Security.Cryptography;

namespace VehicleRegistration.Core.Services
{
    public class UserService : IUserService
    {
        public Task<UserModel> GetUserByIdAsync(Guid UserId)
        {
            throw new NotImplementedException();
        }
        public Task<UserModel> GetUserByNameAsync(string UserName)
        {
            throw new NotImplementedException();
        }
        public Task AddUser(UserModel user)
        {
            throw new NotImplementedException();
        }
        // for creating password hash and salt 
        public (string PasswordHash, string Salt) CreatePasswordHash(string password)
        {
            //creating salt 
            
            return null;
        }
    }
}
