using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace POCCustomerManagement.Controllers
{
	public class AccountController : Controller
	{
		private readonly IConfiguration _config;

		public AccountController(IConfiguration config)
		{
			_config = config;
		}
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginModel model)
		{
			try
			{
				if (ModelState.IsValid && model.Username == "string" && model.Password == "string")
				{
					var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, model.Username)
			};

					var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
					var principal = new ClaimsPrincipal(identity);

					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
					TempData["strMessage"] = "Logged in Successfully !";
					TempData["strMessageCode"] = "1";
					return RedirectToAction("Index", "CustomerEntities");
				}
			}
			catch (Exception ex)
			{
				TempData["strMessage"] = ex.Message;
				TempData["strMessageCode"] = "0";
				return View(model);
			}
			TempData["strMessage"] = "Invalid username or password";
			TempData["strMessageCode"] = "0";
			return View(model);
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			TempData["strMessage"] = "LogOut Successfully !";
			TempData["strMessageCode"] = "1";
			return RedirectToAction("Login");
		}

		private bool ValidateUser(LoginModel login)
		{
			// Replace this with actual validation
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
}
