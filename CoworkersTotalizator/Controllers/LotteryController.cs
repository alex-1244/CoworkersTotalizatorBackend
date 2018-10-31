using System.Collections.Generic;
using System.Linq;
using CoworkersTotalizator.Filters;
using CoworkersTotalizator.Models.Coworkers;
using CoworkersTotalizator.Models.Lotteries.DTO;
using CoworkersTotalizator.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoworkersTotalizator.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AuthorizationMetadata]
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
			return this._lotteryService.GetAll().ToList();
		}

		[HttpPost]
		public ActionResult<int> CreateLottery([FromBody] LotteryDto dto)
		{
			return this._lotteryService.Create(dto);
		}

		[HttpDelete("{id}")]
		[AuthorizationMetadata(true)]
		public ActionResult DeleteLottery(int id)
		{
			this._lotteryService.Delete(id);
			return Ok();
		}

		[HttpGet("assignedCoworkers/{lotteryId}")]
		public ActionResult<IEnumerable<CoworkerDto>> GetAssignedCoworkers(int lotteryId)
		{
			return Ok(this._lotteryService.GetAssignedCoworkers(lotteryId));
		}

		[HttpPost("placeBids/{lotteryId}")]
		public void PlaceBids(int lotteryId, [FromBody] IEnumerable<CoworkerBid> bids)
		{
			this._lotteryService.PlaceBids(lotteryId, bids);
		}

		[HttpGet("{lotteryId}/bids")]
		[AuthorizationMetadata(true)]
		public ActionResult<IEnumerable<CoworkerBid>> GetUsersBids(int lotteryId)
		{
			return Ok(this._lotteryService.GetUsersBids(lotteryId));
		}
	}
}