using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebAPI.Models;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private IConfiguration _config;
        public LoginController(IConfiguration config)
        {
			_config = config;
        }

		private Users AuthenticateUser(Users user)
		{
			Users _user = null;
			if(user.Username=="admin" && user.Password == "12345")
			{
				_user = new Users {Username="manoj Deshmal"};
			}
			return _user;
		}


		 

		private string GenerateToken(Users users)
		{
			var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var credentials = new SigningCredentials(securitykey,SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],null,
				expires:DateTime.Now.AddMinutes(10));
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		[AllowAnonymous]
		[HttpPost]
		
		public IActionResult Login(Users user)
		{
			IActionResult response = Unauthorized();
			var _user = AuthenticateUser(user);
			if (_user != null)
			{
				var token = GenerateToken(_user);
				response = Ok(new { token=token});
			}
			return response;
		}


    }
}
