﻿// <auto-generated />
using System;
using CoworkersTotalizator.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoworkersTotalizator.Dal.Migrations
{
    [DbContext(typeof(CoworkersTotalizatorContext))]
    [Migration("20181021175201_lottery_user_coworker_userBid_nav_properties")]
    partial class lottery_user_coworker_userBid_nav_properties
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoworkersTotalizator.Models.Coworkers.Coworker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<double>("PresenceCoeficient");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Coworkers");
                });

            modelBuilder.Entity("CoworkersTotalizator.Models.Lottery.Lottery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Lotteries");
                });

            modelBuilder.Entity("CoworkersTotalizator.Models.Lottery.LotteryCoworker", b =>
                {
                    b.Property<int>("LotteryId");

                    b.Property<int>("CoworkerId");

                    b.HasKey("LotteryId", "CoworkerId");

                    b.HasIndex("CoworkerId");

                    b.ToTable("LotteryCoworker");
                });

            modelBuilder.Entity("CoworkersTotalizator.Models.Lottery.UserBid", b =>
                {
                    b.Property<int>("LotteryId");

                    b.Property<int>("UserId");

                    b.Property<int>("CoworkerId");

                    b.Property<decimal>("Bid");

                    b.HasKey("LotteryId", "UserId", "CoworkerId");

                    b.HasIndex("CoworkerId");

                    b.HasIndex("UserId");

                    b.ToTable("UserBid");
                });

            modelBuilder.Entity("CoworkersTotalizator.Models.Users.Token", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("TokenHistory");
                });

            modelBuilder.Entity("CoworkersTotalizator.Models.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new { Id = 1, IsAdmin = true, Name = "alex112244@gmail.com" }
                    );
                });

            modelBuilder.Entity("CoworkersTotalizator.Models.Lottery.LotteryCoworker", b =>
                {
                    b.HasOne("CoworkersTotalizator.Models.Coworkers.Coworker", "Coworker")
                        .WithMany()
                        .HasForeignKey("CoworkerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoworkersTotalizator.Models.Lottery.Lottery", "Lottery")
                        .WithMany()
                        .HasForeignKey("LotteryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoworkersTotalizator.Models.Lottery.UserBid", b =>
                {
                    b.HasOne("CoworkersTotalizator.Models.Coworkers.Coworker", "Coworker")
                        .WithMany("UserBids")
                        .HasForeignKey("CoworkerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoworkersTotalizator.Models.Lottery.Lottery", "Lottery")
                        .WithMany("UserBids")
                        .HasForeignKey("LotteryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoworkersTotalizator.Models.Users.User", "User")
                        .WithMany("UserBids")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
