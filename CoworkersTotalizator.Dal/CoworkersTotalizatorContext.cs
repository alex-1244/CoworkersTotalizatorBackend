using System;
using CoworkersTotalizator.Models.Coworkers;
using CoworkersTotalizator.Models.Lottery;
using CoworkersTotalizator.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace CoworkersTotalizator.Dal
{
	public class CoworkersTotalizatorContext : DbContext
	{
		public DbSet<Coworker> Coworkers { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Token> TokenHistory { get; set; }
		public DbSet<Lottery> Lotteries { get; set; }

		public CoworkersTotalizatorContext(DbContextOptions opt) : base(opt)
		{
			var a = 0;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Coworker>()
				 .HasIndex(u => u.Name)
				 .IsUnique();

			modelBuilder.Entity<UserBid>()
				.HasKey(t => new { t.LotteryId, t.UserId, t.CoworkerId });

			modelBuilder.Entity<UserBid>()
				.HasOne(x => x.Lottery)
				.WithMany(x => x.UserBids);

			modelBuilder.Entity<UserBid>()
				.HasOne(x => x.Coworker)
				.WithMany(x => x.UserBids);

			modelBuilder.Entity<UserBid>()
				.HasOne(x => x.User)
				.WithMany(x => x.UserBids);

			modelBuilder.Entity<LotteryCoworker>()
				.HasKey(t => new { t.LotteryId, t.CoworkerId });

			this.Seed(modelBuilder);
		}

		private void Seed(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasData(new User
			{
				Id = 1,
				Name = "alex112244@gmail.com",
				IsAdmin = true
			});
		}
	}
}
