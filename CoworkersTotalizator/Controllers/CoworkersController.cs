using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkersTotalizator.Dal;
using CoworkersTotalizator.Models.Coworkers;
using Microsoft.AspNetCore.Mvc;

namespace CoworkersTotalizator.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CoworkersController : ControllerBase
	{
		private CoworkersTotalizatorContext _context;

		public CoworkersController(CoworkersTotalizatorContext context)
		{
			this._context = context;

		}

		// GET api/values
		[HttpGet]
		public ActionResult<IEnumerable<Coworker>> GetAll()
		{
			return Ok(this._context.Coworkers.ToList());
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<Coworker> Get(int id)
		{
			return Ok(this._context.Coworkers.First(x => x.Id == id));
		}
	}
}
