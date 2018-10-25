using System.Collections.Generic;
using System.Linq;
using CoworkersTotalizator.Filters;
using CoworkersTotalizator.Models.Lotteries.DTO;
using CoworkersTotalizator.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoworkersTotalizator.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AuthorizationMetadata(true)]
	public class LotteryController : ControllerBase
	{
		private readonly LotteryService _lotteryService;

		public LotteryController(
			LotteryService lotteryService)
		{
			this._lotteryService = lotteryService;
		}

		[HttpGet]
		public ActionResult<IEnumerable<LotteryDto>> GetLotteries()
		{
			return this._lotteryService.GetLotteries().ToList();
		}

		[HttpPost]
		public ActionResult<int> CreateLottery([FromBody] LotteryDto dto)
		{
			return this._lotteryService.CreateLotery(dto);
		}
	}
}
