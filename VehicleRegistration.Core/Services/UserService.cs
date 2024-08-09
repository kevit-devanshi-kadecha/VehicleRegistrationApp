using VehicleRegistration.Core.Interfaces;
using VehicleRegistration.Core.DataBaseModels;
using VehicleRegistration.Core.Services;
using System.Security.Cryptography;
using System.Text;

namespace VehicleRegistration.Core.Services
{
    public class UserService : IUserService
    {
        private const int SaltSize = 16; // Size of the salt in bytes
        private const int HashSize = 32; // SHA-256 produces a 32-byte hash
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
            // Generate a salt
            var salt = GenerateSalt();

            // Create a password hash using SHA-256
            var passwordHash = ComputeHash(password, salt);

            // Convert the salt to a Base64 string
            var saltString = Convert.ToBase64String(salt);
            return (passwordHash, saltString);
        }
        private byte[] GenerateSalt()
        {
            var salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
        private string ComputeHash(string password, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);

                // Combine password bytes and salt
                var saltedPasswordBytes = passwordBytes.Concat(salt).ToArray();

                // Compute the hash
                var hashBytes = sha256.ComputeHash(saltedPasswordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}

//var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
//    password: password,
//    salt: salt,
//    prf: KeyDerivation.HMACSHA256,
//    iterationCount: 1000,
//    numBytesRequested: 256 / 8
//    ));
//retu