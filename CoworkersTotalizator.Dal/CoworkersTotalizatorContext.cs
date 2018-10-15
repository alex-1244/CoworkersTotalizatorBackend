using System;
using CoworkersTotalizator.Models.Coworkers;
using CoworkersTotalizator.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace CoworkersTotalizator.Dal
{
	public class CoworkersTotalizatorContext : DbContext
	{
		public DbSet<Coworker> Coworkers { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Token> TokenHistory { get; set; }

		public CoworkersTotalizatorContext(DbContextOptions opt) : base(opt)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Coworker>()
				 .HasIndex(u => u.Name)
				 .IsUnique();

			this.Seed(modelBuilder);
		}

		private void Seed(ModelBuilder modelBuilder)
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
