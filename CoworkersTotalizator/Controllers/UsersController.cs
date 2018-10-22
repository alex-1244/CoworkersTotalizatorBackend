using System.Collections.Generic;
using System.Linq;
using CoworkersTotalizator.Dal;
using CoworkersTotalizator.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace CoworkersTotalizator.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly CoworkersTotalizatorContext _context;

		public UsersController(CoworkersTotalizatorContext context)
		{
			this._context = context;
		}

		[HttpGet]
		public ActionResult<IEnumerable<User>> GetAll()
		{
			return Ok(this._context.Users.ToList());
		}
	}
}
