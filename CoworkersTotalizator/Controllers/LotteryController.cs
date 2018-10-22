using CoworkersTotalizator.Filters;
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
		private readonly LotteryService _lotteryService;

		public LotteryController(
			LotteryService lotteryService)
		{
			this._lotteryService = lotteryService;
		}

		[HttpPost]
		public ActionResult<int> CreateLottery([FromBody] LotteryDto dto)
		{
			return this._lotteryService.CreateLotery(dto);
		}
	}
}
