//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using VehicleRegistration.Core.DTO;
//using VehicleRegistration.Core.Interfaces;


//namespace VehicleRegistration.Core.Services
//{
//    public class JwtService : IJwtService
//    {
//        private readonly IConfiguration _configuration;
//        public JwtService(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public AuthenticationResponse CreateJwtToken(ApplicationUser user)
//        {
//            DateTime expiration = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:EXPIRATION_DAYS"]));
//            Claim[] claims = new Claim[] {
//                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),

//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

//                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

//                new Claim(ClaimTypes.NameIdentifier, user.Email),

//                new Claim(ClaimTypes.Name, user.UserName)
//            };
//            // generateing hash based on key 
//            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Key"]));
//            //using algo for token 
//            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
//            //generating token 
//            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(_configuration["Jwt:Issuer"], expires: expiration, signingCredentials: signingCredentials, claims: claims);
//            //handling the token
//            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
//            string token = handler.WriteToken(jwtSecurityToken);

//            // creating the token 
//            return new AuthenticationResponse()
//            {
//                Token = token,
//                Email = user.Email,
//                Password = user.Password,
//                Expiration = expiration,
//            };
//        }   
//    }
//}
