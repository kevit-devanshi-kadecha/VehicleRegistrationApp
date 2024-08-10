using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VehicleRegistration.Core.DataBaseModels;
using VehicleRegistration.Core.Interfaces;


namespace VehicleRegistration.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthenticationResponse CreateJwtToken(UserModel user)
        {
            DateTime expiration = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:EXPIRATION_DAYS"]));
            
            // details of claims are added in payload 
            Claim[] claims = new Claim[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserEmail),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            // generateing hash based on key 
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //using algo for token and this generates the hash value based on all information 
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //generating token 
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(_configuration["Jwt:Issuer"], expires: expiration, signingCredentials: signingCredentials, claims: claims);
            //handling the token
            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            // creating the token 
            return new AuthenticationResponse()
            {
                Token = token,
                Expiration = expiration,
            };
        }
    }
}
