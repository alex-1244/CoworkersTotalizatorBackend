namespace CoworkersTotalizator.Services
{
	public class CoworkerBidCoeficientSerice
	{
		public double GetBiddingCoeficient(double presenceProbability)
		{
			double presenceProbMin = 0.001;
			double presenceProbMax = 0.999;

			double coeficientMax = 50;
			double coeficientMin = 0.0005;

			double k = (coeficientMin - coeficientMax) / (presenceProbMax - presenceProbMin);
			double b = coeficientMax - k * presenceProbMin;

			return k * presenceProbability + b;
		}
	}
}
