using System.Collections.Generic;
using CoworkersTotalizator.Models.Lottery;

namespace CoworkersTotalizator.Models.Coworkers
{
	public class Coworker
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double PresenceCoeficient { get; set; }

		public ICollection<UserBid> UserBids { get; set; }
	}
}
