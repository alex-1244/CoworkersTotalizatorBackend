﻿// <auto-generated />
using CoworkersTotalizator.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoworkersTotalizator.Dal.Migrations
{
    [DbContext(typeof(CoworkersTotalizatorContext))]
    partial class CoworkersTotalizatorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
#pragma warning restore 612, 618
        }
    }
}
