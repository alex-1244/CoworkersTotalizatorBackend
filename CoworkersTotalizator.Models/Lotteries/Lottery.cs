using System;
using System.Collections.Generic;
using CoworkersTotalizator.Models.Coworkers;

namespace CoworkersTotalizator.Models.Lotteries
{
	public class Lottery
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Date { get; set; }
		public ICollection<UserBid> UserBids { get; set; }
		public ICollection<LotteryCoworker> LotteryCoworkers { get; set; }
	}
}
