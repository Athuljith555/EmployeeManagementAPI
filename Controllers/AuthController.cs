using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
//using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagementAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class AuthController : ControllerBase
    {
        //[HttpGet("CheckUserAccess")]
        //[Authorize]
        //public IActionResult CheckUserAccess()
        //{
        //    // Check if the user has either 'db_owner' or 'db_reader' roles
        //    var hasAccess = User.IsInRole("sa") || User.IsInRole("db_reader");

        //    // Return an object indicating whether the user has access
        //    return Ok(new { Access = hasAccess });
        //}

        //[HttpGet("CheckAdminAccess")]
        //[Authorize]
        //public IActionResult CheckAdminAccess()
        //{
        //    // Check if the user is 'db_owner'
        //    var hasAccess = User.IsInRole("db_owner");

        //    // Return an object indicating whether the user has admin access
        //    return Ok(new { Access = hasAccess });
        //}

        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            // Here you would typically validate the user credentials
            // For demonstration purposes, let's assume the user is valid

            // Mock user data
            var userId = "1";
            var username = "exampleUser";
            var role = "User"; // Retrieve user role from database or another source if needed

            // Create claims for the token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            // Create a key and sign-in credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create a token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time (adjust as needed)
                SigningCredentials = credentials,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            // Create the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return the token
            return Ok(new { Token = tokenHandler.WriteToken(token) });
        }
    }
}
