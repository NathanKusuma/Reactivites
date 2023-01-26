using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService
    {
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),

            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("this is most super secret key with jwt token"));//Creating a key
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
