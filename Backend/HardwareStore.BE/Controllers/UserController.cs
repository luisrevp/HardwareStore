//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using HardwareStore.BE.Entities;
using HardwareStore.BE.Models.User;
using HardwareStore.BE.Services;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace HardwareStore.BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public UserController(SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager ?? throw new ArgumentException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentException(nameof(_userManager));
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] UserDto user)
        {
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest("Couldn't log in...");
            }
            
            User? userToLogin = await _userManager.FindByNameAsync(user.UserName);
            
            if (userToLogin == null && !await _userManager.CheckPasswordAsync(userToLogin, user.Password))
            {
                return Unauthorized("User is not registered!");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberMe, false);

            if (!result.Succeeded) 
            {
                ModelState.AddModelError(string.Empty, "Login attempt failed...");
                return BadRequest(ModelState);
            }

            string JwtBearerToken = this.GenerateJwtToken(userToLogin);
            return Ok(new { JwtBearerToken });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok("Logout successfull!");
            }
            catch (Exception ex)
            {
                return BadRequest($"There was a problem with your request: {ex}");
            }
        }


        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] UserAddDto userToAdd)
        {
            if (userToAdd == null || !ModelState.IsValid)
                return BadRequest("Content couldn't be processed");

            User newUser = new User()
            {
                FirstName = userToAdd.FirstName,
                LastName = userToAdd.LastName,
                Email = userToAdd.Email,
                PasswordHash = userToAdd.Password,
                DoB = userToAdd.DoB,
                UserName = userToAdd.UserName
            };

            try
            {
                var result = await _userManager.CreateAsync(newUser, userToAdd.Password);

                if (!result.Succeeded)
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return BadRequest(ModelState);
                }

                await _signInManager.SignInAsync(newUser, isPersistent: false);
                return Accepted();
            }
            catch (Exception ex)
            {
                return Problem($"There was an error processing your request: {ex.Message}");
            }
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var configSection = _configuration.GetSection("JwtSettings");

            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configSection["Key"] ?? string.Empty));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                configSection["Issuer"],
                configSection["Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
