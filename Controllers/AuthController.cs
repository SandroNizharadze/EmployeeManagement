using EmployeePortal.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration; // This will access the appsettings.json configuration
        }

        // Registration Endpoint
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                if (string.IsNullOrEmpty(model.Password))
                {
                    return BadRequest("Password cannot be null or empty.");
                }
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "User created successfully" });
                }
                return BadRequest(result.Errors);
            }
            return BadRequest("Invalid data.");
        }

        // Login Endpoint (Generates JWT Token)
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return BadRequest("Email cannot be null or empty.");
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && !string.IsNullOrEmpty(model.Password) && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                // Create JWT claims based on user data
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                    new Claim("role", "Admin") // Example custom claim (role)
                };

                // Get JWT settings from configuration
                var jwtKey = _configuration["Jwt:Key"];
                if (string.IsNullOrEmpty(jwtKey))
                {
                    return StatusCode(500, "JWT Key is not configured properly.");
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)); // The secret key from appsettings.json
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Create the token
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"], // The Issuer from appsettings.json
                    audience: _configuration["Jwt:Audience"], // The Audience from appsettings.json
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30), // Token expiration time (e.g., 30 minutes)
                    signingCredentials: creds
                );

                // Return the JWT token
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return Unauthorized(); // If user authentication fails
        }
    }
}