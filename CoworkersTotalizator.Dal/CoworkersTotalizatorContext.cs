using System;
using CoworkersTotalizator.Models.Coworkers;
using CoworkersTotalizator.Models.Lotteries;
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
		public DbSet<LotteryUserResult> LotteryUserResults { get; set; }

		public CoworkersTotalizatorContext(DbContextOptions opt) : base(opt)
		{
			var a = 0;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			MapUserBidEntity(modelBuilder);
			MapLotteryUserResultEntity(modelBuilder);

			modelBuilder.Entity<Coworker>()
				 .HasIndex(u => u.Name)
				 .IsUnique();

			modelBuilder.Entity<Coworker>()
				.HasMany(x => x.UserBids)
				.WithOne(x => x.Coworker)
				.HasForeignKey(x => x.CoworkerId);

			modelBuilder.Entity<Lottery>()
				.HasMany(x => x.UserBids)
				.WithOne(x => x.Lottery)
				.HasForeignKey(x => x.LotteryId);

			modelBuilder.Entity<LotteryCoworker>()
				.HasKey(t => new { t.LotteryId, t.CoworkerId });

			modelBuilder.Entity<LotteryCoworker>()
				.HasOne(x => x.Lottery)
				.WithMany(x => x.LotteryCoworkers)
				.HasForeignKey(x => x.LotteryId);

			modelBuilder.Entity<LotteryCoworker>()
				.HasOne(x => x.Coworker)
				.WithMany(x => x.LotteryCoworkers)
				.HasForeignKey(x => x.CoworkerId);

			this.Seed(modelBuilder);
		}

		private void MapLotteryUserResultEntity(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<LotteryUserResult>()
				.HasKey(t => new { t.LotteryId, t.UserId, t.CoworkerId });

			modelBuilder.Entity<LotteryUserResult>()
				.HasOne(x => x.Lottery)
				.WithMany(x => x.LotteryUserResults);

			modelBuilder.Entity<LotteryUserResult>()
				.HasOne(x => x.Coworker)
				.WithMany(x => x.LotteryUserResults)
				.HasForeignKey(x => x.CoworkerId);

			modelBuilder.Entity<LotteryUserResult>()
				.HasOne(x => x.User)
				.WithMany(x => x.LotteryUserResults);
		}

		private void MapUserBidEntity(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserBid>()
				.HasKey(t => new { t.LotteryId, t.UserId, t.CoworkerId });

			modelBuilder.Entity<UserBid>()
				.HasOne(x => x.Lottery)
				.WithMany(x => x.UserBids);

			modelBuilder.Entity<UserBid>()
				.HasOne(x => x.Coworker)
				.WithMany(x => x.UserBids)
				.HasForeignKey(x => x.CoworkerId);

			modelBuilder.Entity<UserBid>()
				.HasOne(x => x.User)
				.WithMany(x => x.UserBids);
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
