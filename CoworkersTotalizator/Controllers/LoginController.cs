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
		private CoworkersTotalizatorContext _context;
		private LoginService _loginService;

		public LoginController(
			CoworkersTotalizatorContext context,
			LoginService loginService)
		{
			this._context = context;
			this._loginService = loginService;
		}

		[HttpPost]
		public ActionResult<object> Login([FromBody] string userName)
		{
			this._loginService.GetToken(userName);
			return Ok();
		}

		[HttpGet("{id}")]
		public ActionResult<Coworker> Get(int id)
		{
			return Ok(this._context.Coworkers.First(x => x.Id == id));
		}
	}
}
