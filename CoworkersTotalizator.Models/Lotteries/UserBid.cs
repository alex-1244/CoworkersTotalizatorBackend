using CoworkersTotalizator.Models.Coworkers;
using CoworkersTotalizator.Models.Users;

namespace CoworkersTotalizator.Models.Lotteries
{
	public class UserBid
	{
		public int LotteryId { get; set; }
		public Lottery Lottery { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
		public int CoworkerId { get; set; }
		public Coworker Coworker { get; set; }
		public decimal Bid { get; set; }
	}
}
