using System;
using System.Collections.Generic;
using System.Linq;
using CoworkersTotalizator.Dal;
using CoworkersTotalizator.Models.Coworkers;
using CoworkersTotalizator.Models.Lottery.DTO;
using CoworkersTotalizator.Models.Users;
using CoworkersTotalizator.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CoworkersTotalizator.Tests.Integration
{
	public class LotteryServiceTests
	{
		[Fact]
		public void CreateLotery_WithCorrectArgs_CreatesLottery()
		{
			var optBuilder = new DbContextOptionsBuilder();
			optBuilder.UseSqlServer("Server=192.168.1.106,1433;Database=CoworkersTotalizator_test;User Id=sa;Password=password1;");

			var context = new CoworkersTotalizatorContext(optBuilder.Options);
			context.Database.EnsureDeleted();
			context.Database.Migrate();

			LotteryService ls = new LotteryService(context);

			context.Users.Add(new User
			{
				Name = "Alex",
				IsAdmin = true
			});

			context.Coworkers.Add(new Coworker
			{
				Name = "coworker1",
				PresenceCoeficient = 0.9,
			});

			context.SaveChanges();

			ls.CreateLotery(new LotteryDto
			{
				Name = "Lottery1",
				Date = DateTime.Now,
				CoworkerIds = new[] { 1 },
				UserBids = new Dictionary<int, CoworkerBid>()
				{
					{ 1, new CoworkerBid{ Bid = 0.5m, CowrkerId = 1} }
				}
			});
			
			Assert.Equal(1, context.Users.First().UserBids.First().CoworkerId);
		}
	}
}
