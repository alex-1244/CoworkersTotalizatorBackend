using System;
using System.Linq;
using CoworkersTotalizator.Dal;
using CoworkersTotalizator.Models.Coworkers;
using CoworkersTotalizator.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoworkersTotalizator.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly CoworkersTotalizatorContext _context;
		private readonly LoginService _loginService;

		public LoginController(
			CoworkersTotalizatorContext context,
			LoginService loginService)
		{
			this._context = context;
			this._loginService = loginService;
		}

		[HttpPost("login")]
		public ActionResult<object> Login([FromBody] string userName)
		{
			this._loginService.GetToken(userName);
			return Ok();
		}

		[HttpPost("validate")]
		public ActionResult<object> Validate([FromBody] Guid token)
		{
			var twentyMinutesAgo = DateTime.UtcNow - TimeSpan.FromMinutes(20);
			if (this._context.TokenHistory.Any(x => x.Id == token && x.CreatedAt >= twentyMinutesAgo))
			{
				return Ok();
			}

			return BadRequest();
		}

		[HttpGet("{id}")]
		public ActionResult<Coworker> Get(int id)
		{
			return Ok(this._context.Coworkers.First(x => x.Id == id));
		}
	}
}
