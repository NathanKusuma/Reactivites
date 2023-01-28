using API.DTOs;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers
{
[ApiController]
[Route("api/[controller]")]
    public class AccountController:ControllerBase
    {
        public UserManager<AppUser> _userManager { get; }
        public TokenService _tokenService { get; }
     public AccountController(UserManager<AppUser> userManager,TokenService tokenService)
     {
            _tokenService = tokenService;
            _userManager = userManager;
        
     } 

     [AllowAnonymous] //ensure that all of endpoints no longer needs authentication  
     [HttpPost("login")]
     public async Task<ActionResult<UserDto>>Login(LoginDto loginDto){
        var user= await _userManager.FindByEmailAsync(loginDto.Email);
        if(user==null) return Unauthorized();
        var result=await _userManager.CheckPasswordAsync(user,loginDto.Password);
        if(result)
        {
            return CreateUserObject(user);
        }
        else
        {

        return Unauthorized();
        }
     }
     
    [AllowAnonymous] //ensure that all of endpoints no longer needs authentication
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>>Register(RegisterDto registerDto)
    {
        if(await _userManager.Users.AnyAsync(x=>x.UserName == registerDto.UserName))
        {
            return BadRequest("Username is already taken");
        }
        if(await _userManager.Users.AnyAsync(x=>x.Email == registerDto.Email))
        {
            return BadRequest("Email already taken");
        }
        var user = new AppUser
        {
            DisplayName=registerDto.DisplayName,
            Email=registerDto.Email,
            UserName=registerDto.UserName
        };
        var result= await _userManager.CreateAsync(user,registerDto.Password);

        if (result.Succeeded)
            {
                return CreateUserObject(user);
            }
            else
            {
            return BadRequest(result.Errors);
            }
        }

        
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>>GetCurrentUser()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            return CreateUserObject(user);
        }

        private UserDto CreateUserObject(AppUser user)
        {
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Image = null,
                Token = _tokenService.CreateToken(user),
                UserName = user.UserName

            };
        }
    
    }
}