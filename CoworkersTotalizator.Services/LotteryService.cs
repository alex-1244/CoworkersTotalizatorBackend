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

		public IEnumerable<LotteryDto> GetAll()
		{
			return this._context.Lotteries.Select(x => new LotteryDto
			{
				Id = x.Id,
				Name = x.Name,
				Date = x.Date,
				CoworkerIds = x.LotteryCoworkers.Select(c => c.CoworkerId)
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
	}
}
