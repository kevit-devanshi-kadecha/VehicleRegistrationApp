using VehicleRegistration.Core.DataBaseModels;

namespace VehicleRegistration.Core.Interfaces
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(UserModel user);
    }
}
