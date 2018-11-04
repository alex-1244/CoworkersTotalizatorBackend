using System.Collections.Generic;
using CoworkersTotalizator.Models.Lotteries;

namespace CoworkersTotalizator.Models.Coworkers
{
	public class Coworker
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double PresenceCoeficient { get; set; }
		public bool IsDeleted { get; set; }

		public ICollection<UserBid> UserBids { get; set; }
		public ICollection<LotteryCoworker> LotteryCoworkers { get; set; }
		public ICollection<LotteryUserResult> LotteryUserResults { get; set; }
	}

	public class CoworkerDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double PresenceCoeficient { get; set; }
	}
}
