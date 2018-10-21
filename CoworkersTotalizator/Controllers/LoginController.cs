using System;
using CoworkersTotalizator.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoworkersTotalizator.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly LoginService _loginService;

		public LoginController(LoginService loginService)
		{
			this._loginService = loginService;
		}

		[HttpPost("login")]
		public ActionResult Login([FromBody] string userName)
		{
			this._loginService.GetToken(userName);
			return Ok();
		}

		[HttpPost("validate")]
		public ActionResult Validate([FromBody] Guid token)
		{
			if (this._loginService.Validate(token))
			{
				return Ok();
			}

			return BadRequest();
		}
	}
}
