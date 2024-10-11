using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace POCCustomerManagement.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : Controller
	{
		private readonly IConfiguration _config;

		public AuthController(IConfiguration config)
		{
			_config = config;
		}

		[HttpPost("login")]
		public IActionResult Login([FromBody] LoginModel login)
		{
			if (ValidateUser(login))
			{
				var token = GenerateToken(login);
				return Ok(new { token });
			}
			return Unauthorized();
		}

		private bool ValidateUser(LoginModel login)
		{
			// Replace with actual user validation
			return login.Username == "string" && login.Password == "string";
		}

		private string GenerateToken(LoginModel user)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier,user.Username),
			};
			var token = new JwtSecurityToken(_config["Jwt:Issuer"],
				_config["Jwt:Audience"],
				claims,
				expires: DateTime.Now.AddMinutes(15),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);

		}
	}
	public class LoginModel
	{
        [Required]
        public string Username { get; set; }
        [Required]
		public string Password { get; set; }
	}
}
