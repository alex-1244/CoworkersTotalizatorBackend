using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoworkersTotalizator.Models.Users
{
	public class Token
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public Guid Id { get; set; }
		public string UserId { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
