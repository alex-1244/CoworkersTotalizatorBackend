using CoworkersTotalizator.Dal;
using CoworkersTotalizator.Models.Lottery;
using CoworkersTotalizator.Models.Lottery.DTO;
using System.Collections.Generic;
using System.Linq;

namespace CoworkersTotalizator.Services
{
	public class LotteryService
	{
		private readonly CoworkersTotalizatorContext _context;

		public LotteryService(CoworkersTotalizatorContext context)
		{

			this._context = context;
		}

		public int CreateLotery(LotteryDto lotteryDto)
		{
			var lottery = new Lottery
			{
				Name = lotteryDto.Name,
				Date = lotteryDto.Date
			};

			this._context.Lotteries.Add(lottery);

			this._context.AddRange(lotteryDto.CoworkerIds.Select(x => new LotteryCoworker
			{
				Lottery = lottery,
				CoworkerId = x
			}));

			lottery.UserBids = lotteryDto.UserBids.ToList().Select(x => new UserBid
			{
				Lottery = lottery,
				UserId = x.Key,
				CoworkerId = x.Value.CowrkerId,
				Bid = x.Value.Bid
			}).ToList();

			this._context.SaveChanges();

			return lottery.Id;
		}
	}
}
