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
        private readonly UserManager<Employee> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        public AuthController(UserManager<Employee> userManager, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _configuration = configuration; // This will access the appsettings.json configuration
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration; // This will access the appsettings.json configuration
        }

        // Registration Endpoint
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Email == null || model.PhoneNumber == null || model.Salary == null)
                {
                    return BadRequest("Email, Phone Number, and Salary cannot be null.");
                }
                var user = new Employee { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber, Salary = model.Salary };
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
            if (user == null)
            {
                _logger.LogWarning("User not found: {Email}", model.Email);
                return Unauthorized(new { Message = "Invalid credentials" });
            }

            if (string.IsNullOrEmpty(model.Password) || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                _logger.LogWarning("Invalid password for user: {Email}", model.Email);
                return Unauthorized(new { Message = "Invalid credentials" });
            }

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secret = _configuration["JwtSettings:Secret"];
            if (string.IsNullOrEmpty(secret))
            {
                _logger.LogError("JWT Secret is not configured.");
                return StatusCode(500, "Internal server error");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}