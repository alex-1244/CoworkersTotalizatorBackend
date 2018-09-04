using CoworkersTotalizator.Models.Coworkers;
using Microsoft.EntityFrameworkCore;

namespace CoworkersTotalizator.Dal
{
	public class CoworkersTotalizatorContext : DbContext
	{
		public DbSet<Coworker> Coworkers { get; set; }

		public CoworkersTotalizatorContext(DbContextOptions opt) : base(opt)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Coworker>().HasData(new Coworker
			{
				Id = 1,
				Name = "Alex T",
				PresenceCoeficient = 0.9
			});
		}
	}
}
