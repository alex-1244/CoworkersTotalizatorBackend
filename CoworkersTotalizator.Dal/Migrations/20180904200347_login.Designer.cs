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
    [Migration("20180904200347_login")]
    partial class login
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
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

                    b.ToTable("Coworkers");

                    b.HasData(
                        new { Id = 1, Name = "Alex T", PresenceCoeficient = 0.9 }
                    );
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
                });
#pragma warning restore 612, 618
        }
    }
}