using API.Services;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Persistence;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
       public static IServiceCollection AddIdentityServices (this IServiceCollection services,IConfiguration config)
       {
        services.AddIdentityCore<AppUser>(opt =>{
            opt.Password.RequireNonAlphanumeric = false;
        })
        .AddEntityFrameworkStores<DataContext>();

         var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("this is most super secret key with jwt token"));
                
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt=>{
            opt.TokenValidationParameters=new TokenValidationParameters
            {
                ValidateIssuerSigningKey=true,
                IssuerSigningKey=key,
                ValidateIssuer=false,
                ValidateAudience=false
            };
        });
        services.AddScoped<TokenService>();//using token service that going to be scoped to the http itself
        return services;
       }
    }
}