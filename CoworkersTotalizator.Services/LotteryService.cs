using CoworkersTotalizator.Dal;
using CoworkersTotalizator.Models.Lotteries;
using CoworkersTotalizator.Models.Lotteries.DTO;
using System.Collections.Generic;
using System.Linq;
using CoworkersTotalizator.Models.Coworkers;
using Microsoft.EntityFrameworkCore;

namespace CoworkersTotalizator.Services
{
	public class LotteryService
	{
		private readonly CoworkersTotalizatorContext _context;
		private ICurrentUserAccessor _currentUserAccessor;

		public LotteryService(
			CoworkersTotalizatorContext context,
			ICurrentUserAccessor currentUserAccessor)
		{

			this._context = context;
			this._currentUserAccessor = currentUserAccessor;
		}

		public IEnumerable<LotteryDto> GetAll()
		{
			var crrentUserId = this._currentUserAccessor.GetCurrentUser().Id;

			return this._context.Lotteries
				.Include(x => x.UserBids)
				.Include(x => x.LotteryCoworkers)
				.Select(x => new LotteryDto
				{
					Id = x.Id,
					Name = x.Name,
					Date = x.Date,
					CoworkerIds = x.LotteryCoworkers.Select(c => c.CoworkerId),
					CoworkerBids = x.UserBids.Where(b => b.UserId == crrentUserId).Select(userBid => new CoworkerBid
					{
						CoworkerId = userBid.CoworkerId,
						BidAmmount = userBid.Bid
					})
				});
		}

		public int Create(LotteryDto lotteryDto)
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

			this._context.SaveChanges();

			return lottery.Id;
		}

		public void Delete(int id)
		{
			this._context.Lotteries.Remove(new Lottery { Id = id });
			this._context.SaveChanges();
		}

		public IEnumerable<CoworkerDto> GetAssignedCoworkers(int lotteryId)
		{
			var coworkersQ = this._context.Lotteries.Include(x => x.LotteryCoworkers).ThenInclude(x => x.Coworker);
			var lottery = coworkersQ.First(x => x.Id == lotteryId);
			return lottery.LotteryCoworkers.ToList().Select(x => x.Coworker).Select(x => new CoworkerDto
			{
				Id = x.Id,
				Name = x.Name,
				PresenceCoeficient = x.PresenceCoeficient
			});
		}

		public void PlaceBids(int lotteryId, IEnumerable<CoworkerBid> bids)
		{
			var lottery = this._context.Lotteries.Include(x => x.UserBids).First(x => x.Id == lotteryId);

			foreach (var bid in bids)
			{
				var coworker = new Coworker { Id = bid.CoworkerId };
				this._context.Coworkers.Attach(coworker).State = EntityState.Unchanged;
				var user = this._currentUserAccessor.GetCurrentUser();

				var existingBid = lottery.UserBids.FirstOrDefault(x =>
					x.CoworkerId == bid.CoworkerId
					&& x.UserId == user.Id);

				if (existingBid == null)
				{
					var userBid = new UserBid()
					{
						LotteryId = lotteryId,
						Lottery = lottery,
						Bid = bid.BidAmmount,
						CoworkerId = bid.CoworkerId,
						Coworker = coworker,
						User = user
					};

					lottery.UserBids.Add(userBid);
				}
				else
				{
					existingBid.Bid = bid.BidAmmount;
				}
			}

			this._context.SaveChanges();
		}

		public IEnumerable<CoworkerBid> GetUsersBids(int lotteryId)
		{
			return this._context.Lotteries
				.Include(x=>x.UserBids)
				.Single(x=>x.Id == lotteryId)
				.UserBids
				.Select(userBid => new CoworkerBid
				{
					CoworkerId = userBid.CoworkerId,
					BidAmmount = userBid.Bid
				});
		}
	}
}
