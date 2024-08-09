using VehicleRegistration.Core.DataBaseModels;

namespace VehicleRegistration.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> GetUserByIdAsync(Guid userId);
        Task<UserModel> GetUserByNameAsync(string userName);
        Task AddUser(UserModel user);
        (string PasswordHash, string Salt) CreatePasswordHash(string password);
    }
}
