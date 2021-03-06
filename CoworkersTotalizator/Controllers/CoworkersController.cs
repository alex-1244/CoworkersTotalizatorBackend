﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkersTotalizator.Dal;
using CoworkersTotalizator.Filters;
using CoworkersTotalizator.Models.Coworkers;
using Microsoft.AspNetCore.Mvc;

namespace CoworkersTotalizator.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AuthorizationMetadata]
	public class CoworkersController : ControllerBase
	{
		private readonly CoworkersTotalizatorContext _context;

		public CoworkersController(CoworkersTotalizatorContext context)
		{
			this._context = context;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Coworker>> GetAll()
		{
			return Ok(this._context.Coworkers.Where(x => !x.IsDeleted).ToList());
		}

		[HttpGet("{id}")]
		public ActionResult<Coworker> Get(int id)
		{
			return Ok(this._context.Coworkers.First(x => x.Id == id));
		}

		[HttpPost]
		public async Task<ActionResult<int>> Create([FromBody] Coworker newCoworker)
		{
			if (this._context.Coworkers.Any(x => x.Name == newCoworker.Name))
			{
				return BadRequest("Coworker with such name already exisits");
			}

			this._context.Coworkers.Add(newCoworker);
			await this._context.SaveChangesAsync();
			return Ok(newCoworker.Id);
		}

		[HttpDelete("{id}")]
		[AuthorizationMetadata(true)]
		public async Task<ActionResult> Delete(int id)
		{
			var coworker = this._context.Coworkers.Find(id);
			coworker.IsDeleted = true;

			await this._context.SaveChangesAsync();
			return Ok();
		}
	}
}
