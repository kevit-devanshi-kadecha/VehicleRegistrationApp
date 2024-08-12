using VehicleRegistration.Infrastructure.DataBaseModels;

namespace VehicleRegistration.Core.Interfaces
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(UserModel user);
    }
}
