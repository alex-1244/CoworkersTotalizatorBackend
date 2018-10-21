using CoworkersTotalizator.Models.Coworkers;

namespace CoworkersTotalizator.Models.Lottery
{
	public class LotteryCoworker
	{
		public int LotteryId { get; set; }
		public  Lottery Lottery { get; set; }
		public int CoworkerId { get; set; }
		public Coworker Coworker { get; set; }
	}
}
