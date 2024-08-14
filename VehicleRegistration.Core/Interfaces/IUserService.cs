using VehicleRegistration.Infrastructure.DataBaseModels;

namespace VehicleRegistration.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> GetUserByNameAsync(string userName);
        Task<UserModel> GetUserBYEmaiIdAsync(string userEmail);
        Task<(string PasswordHash, string Salt)> GetPasswordHashAndSalt(string userName);
        Task AddUser(UserModel user, string plainPassword);
        Task<bool> AuthenticateUser(string userName, string plainPassword);
        (string PasswordHash, string Salt) CreatePasswordHash(string password);
    }
}
