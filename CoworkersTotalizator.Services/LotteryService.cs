using CoworkersTotalizator.Dal;
using CoworkersTotalizator.Models.Lotteries;
using CoworkersTotalizator.Models.Lotteries.DTO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CoworkersTotalizator.Services
{
	public class LotteryService
	{
		private readonly CoworkersTotalizatorContext _context;

		public LotteryService(CoworkersTotalizatorContext context)
		{

			this._context = context;
		}

		public IEnumerable<LotteryDto> GetLotteries()
		{
			var userBids = this._context.Lotteries.Include(x=>x.UserBids).First().UserBids.ToDictionary(k => k.UserId, v => new CoworkerBid
			{
				Bid = v.Bid,
				CowrkerId = v.CoworkerId
			});

			return this._context.Lotteries.Select(x => new LotteryDto
			{
				Name = x.Name,
				Date = x.Date,
				UserBids = x.UserBids.ToDictionary(k => k.UserId, v => new CoworkerBid
				{
					Bid = v.Bid,
					CowrkerId = v.CoworkerId
				}),
				CoworkerIds = x.LotteryCoworkers.Select(c => c.CoworkerId)
			});
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

			//lottery.UserBids = lotteryDto.UserBids.ToList().Select(x => new UserBid
			//{
			//	Lottery = lottery,
			//	UserId = x.Key,
			//	CoworkerId = x.Value.CowrkerId,
			//	Bid = x.Value.Bid
			//}).ToList();

			this._context.SaveChanges();

			return lottery.Id;
		}
	}
}
