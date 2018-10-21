using System;
using System.Collections.Generic;
using System.Text;

namespace CoworkersTotalizator.Models.Lottery
{
	public class Lottery
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Date { get; set; }
		public ICollection<UserBid> UserBids { get; set; }
	}
}
