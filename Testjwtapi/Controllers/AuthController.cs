using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Testjwtapi.Models;


namespace Testjwtapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        //public IActionResult Login(string username, string password)
        public IActionResult Login(LoginRequest request)
        {
            // For learning only
            if (request.Username != "admin" || request.Password != "1234")
            {
                return Unauthorized("Invalid username or password");
            }

            // Create user information
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Username)
            };

            // Our secret key
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("my-secret-key-for-learning-12345"));

            // Create signing credentials
            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            // Create JWT
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            // Convert token to string
            var tokenString = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return Ok(new
            {
                token = tokenString
            });
        }
    }
}
