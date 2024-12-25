using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TreasuryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _jwtSettings;

        public AuthController(IConfiguration jwtSettings)
        {
            _jwtSettings = jwtSettings.GetSection("JwtSettings");
        }

        [HttpPost("token")]
        public IActionResult GenerateToken()
        {
            var secretKey = _jwtSettings["SecretKey"];
            Console.WriteLine($"Secret Key: {secretKey}");

            if (string.IsNullOrEmpty(secretKey))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Secret key not configured.");
            }

            var key = Encoding.UTF8.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("role", "User") }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }

    }
}
