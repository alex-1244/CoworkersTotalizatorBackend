using System;
using System.Collections.Generic;
using System.Text;

namespace CoworkersTotalizator.Models.Lottery.DTO
{
	public class LotteryDto
	{
		public string Name { get; set; }
		public DateTime Date { get; set; }
		public IEnumerable<int> CoworkerIds { get; set; }
		public IDictionary<int, CoworkerBid> UserBids { get; set; }
	}

	public class CoworkerBid
	{
		public int CowrkerId { get; set; }
		public decimal Bid { get; set; }
	}
}
