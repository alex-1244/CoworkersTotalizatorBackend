using System;
using System.Collections.Generic;
using CoworkersTotalizator.Models.Coworkers;
using CoworkersTotalizator.Models.Users;

namespace CoworkersTotalizator.Models.Lotteries
{
	public class Lottery
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Date { get; set; }
		public bool IsFinished { get; set; }

		public ICollection<UserBid> UserBids { get; set; }
		public ICollection<LotteryCoworker> LotteryCoworkers { get; set; }
		public ICollection<LotteryUserResult> LotteryUserResults { get; set; }
	}

	public class LotteryUserResult
	{
		public int LotteryId { get; set; }
		public Lottery Lottery { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
		public int CoworkerId { get; set; }
		public Coworker Coworker { get; set; }
		public double Gain { get; set; }
	}
}
