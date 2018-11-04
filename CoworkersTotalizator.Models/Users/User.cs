using System.Collections.Generic;
using CoworkersTotalizator.Models.Lotteries;

namespace CoworkersTotalizator.Models.Users
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsAdmin {get; set; }

		public ICollection<UserBid> UserBids { get; set; }
		public ICollection<LotteryUserResult> LotteryUserResults { get; set; }
	}
}
