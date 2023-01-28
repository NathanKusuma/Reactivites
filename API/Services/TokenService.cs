using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
            
        }

        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),

            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["TokenKey"]));//Creating a key, TokenKey is object in appsetting.development.json
            var creds= new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);//Creating a credentials
            var tokenDescriptor = new  SecurityTokenDescriptor
            {
                Subject= new ClaimsIdentity(claims),
                Expires= DateTime.UtcNow.AddDays(7),
                SigningCredentials=creds
            };//Store our key

            var tokenHandler= new JwtSecurityTokenHandler();//token handler

            var token=tokenHandler.CreateToken(tokenDescriptor);  //Create our token

            return tokenHandler.WriteToken(token);

        }
    }
}

//Note
//SymmetricSecurityKey is when the same key is used for encryption and decryption
//with SymmetricSecurityKey,the same key that use for encrypt also used for decrypt it
//while asymmetricsecuritykey is the opposite
