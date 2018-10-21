using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkersTotalizator.Dal;
using CoworkersTotalizator.Filters;
using CoworkersTotalizator.Models.Coworkers;
using CoworkersTotalizator.Models.Lottery.DTO;
using CoworkersTotalizator.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoworkersTotalizator.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AuthorizationMetadata(true)]
	public class LotteryController : ControllerBase
	{
		private readonly CoworkersTotalizatorContext _context;
		private readonly LotteryService _lotteryService;

		public LotteryController(
			CoworkersTotalizatorContext context,
			LotteryService lotteryService)
		{
			this._context = context;
			this._lotteryService = lotteryService;
		}

		[HttpPost]
		public ActionResult<int> CreateLottery([FromBody] LotteryDto dto)
		{
			return this._lotteryService.CreateLotery(dto);
		}
	}
}
