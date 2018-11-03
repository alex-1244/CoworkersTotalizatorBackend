using System;
using System.Collections.Generic;

namespace CoworkersTotalizator.Models.Lotteries.DTO
{
	public class LotteryDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Date { get; set; }
		public IEnumerable<int> CoworkerIds { get; set; }
		public IEnumerable<CoworkerBid> CoworkerBids { get; set; }
	}

	public class CoworkerBid
	{
		public int UserId { get; set; }
		public int CoworkerId { get; set; }
		public decimal BidAmmount { get; set; }
	}
}
