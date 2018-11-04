using System;
using System.Runtime.Serialization;

namespace CoworkersTotalizator.Models.Exceptions
{
	public class LotteryIsFinishedException : Exception
	{
		public LotteryIsFinishedException()
		{
		}

		public LotteryIsFinishedException(string message) : base(message)
		{
		}

		public LotteryIsFinishedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected LotteryIsFinishedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
