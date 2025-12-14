using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieReview.Dto;
using MovieReview.Interfaces;
using MovieReview.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        public AuthController(IUserRepository userRepository,IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;

        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            if (login == null)
                return BadRequest("Invalid login request.");

           
            var user = _userRepository.GetUser(login.Username, login.Password);
            if (user == null)
                return Unauthorized("Invalid username or password.");

            //  Read JWT settings from appsettings
            var jwtSection = _config.GetSection("Jwt");
            var key = jwtSection["Key"];
            var issuer = jwtSection["Issuer"];
            var audience = jwtSection["Audience"];

            // IMPORTANT: ClaimTypes.Role enables [Authorize(Roles = "Admin")]
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            //  Sign the token (proves it was created by your API)
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            //  Create the JWT
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            //  Return token string to the client
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                role = user.Role
            });
        }
    }
}
