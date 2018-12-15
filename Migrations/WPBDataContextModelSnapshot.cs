﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WePlayBall.Data;

namespace weplayball.Migrations
{
    [DbContext(typeof(WPBDataContext))]
    partial class WPBDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WePlayBall.Models.DataSourceFixture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClassNameNode")
                        .IsRequired();

                    b.Property<string>("DataSourceDescription")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("Division")
                        .IsRequired();

                    b.Property<string>("DivisionCode")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("UrlHash")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("DataSourceFixture");
                });

            modelBuilder.Entity("WePlayBall.Models.DataSourceRanking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClassNameNode")
                        .IsRequired();

                    b.Property<string>("DataSourceDescription")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("Division")
                        .IsRequired();

                    b.Property<string>("DivisionCode")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("UrlHash")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("DataSourceRanking");
                });

            modelBuilder.Entity("WePlayBall.Models.DataSourceResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClassNameNode")
                        .IsRequired();

                    b.Property<string>("DataSourceDescription")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("Division")
                        .IsRequired();

                    b.Property<string>("DivisionCode")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("Url")
                        .IsRequired();

                    b.Property<string>("UrlHash")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("DataSourceResult");
                });

            modelBuilder.Entity("WePlayBall.Models.Division", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DivisionCode")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<string>("DivisionName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Division");
                });

            modelBuilder.Entity("WePlayBall.Models.Fixture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AwayTeamCode")
                        .IsRequired();

                    b.Property<int>("AwayTeamId");

                    b.Property<string>("AwayTeamName")
                        .IsRequired();

                    b.Property<DateTime>("FixtureDate");

                    b.Property<string>("HomeTeamCode")
                        .IsRequired();

                    b.Property<int>("HomeTeamId");

                    b.Property<string>("HomeTeamName")
                        .IsRequired();

                    b.Property<int>("SubDivisionId");

                    b.HasKey("Id");

                    b.HasIndex("SubDivisionId");

                    b.ToTable("Fixture");
                });

            modelBuilder.Entity("WePlayBall.Models.GameResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AwayTeamCode")
                        .IsRequired();

                    b.Property<int>("AwayTeamId");

                    b.Property<string>("AwayTeamName")
                        .IsRequired();

                    b.Property<string>("EncodedResult")
                        .IsRequired();

                    b.Property<string>("HomeTeamCode")
                        .IsRequired();

                    b.Property<int>("HomeTeamId");

                    b.Property<string>("HomeTeamName")
                        .IsRequired();

                    b.Property<string>("Score")
                        .IsRequired();

                    b.Property<int>("SubDivisionId");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("WinningTeamCode")
                        .IsRequired();

                    b.Property<string>("WinningTeamName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("SubDivisionId");

                    b.ToTable("GameResult");
                });

            modelBuilder.Entity("WePlayBall.Models.Rank", b =>
                {
                    b.Property<int>("TeamId");

                    b.Property<string>("GamesLost")
                        .IsRequired();

                    b.Property<string>("GamesPlayed")
                        .IsRequired();

                    b.Property<int>("GamesWon");

                    b.Property<int>("Points");

                    b.Property<int>("Position");

                    b.Property<int>("SubDivisionId");

                    b.HasKey("TeamId");

                    b.HasIndex("SubDivisionId");

                    b.ToTable("Rank");
                });

            modelBuilder.Entity("WePlayBall.Models.SubDivision", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DivisionId");

                    b.Property<string>("SubDivisionCode")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<string>("SubDivisionTitle")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DivisionId");

                    b.ToTable("SubDivision");
                });

            modelBuilder.Entity("WePlayBall.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SubDivisionId");

                    b.Property<string>("TeamCode")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("SubDivisionId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("WePlayBall.Models.Fixture", b =>
                {
                    b.HasOne("WePlayBall.Models.SubDivision", "SubDivision")
                        .WithMany()
                        .HasForeignKey("SubDivisionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WePlayBall.Models.GameResult", b =>
                {
                    b.HasOne("WePlayBall.Models.SubDivision", "SubDivision")
                        .WithMany()
                        .HasForeignKey("SubDivisionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WePlayBall.Models.Rank", b =>
                {
                    b.HasOne("WePlayBall.Models.SubDivision", "SubDivision")
                        .WithMany()
                        .HasForeignKey("SubDivisionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WePlayBall.Models.SubDivision", b =>
                {
                    b.HasOne("WePlayBall.Models.Division", "Division")
                        .WithMany("SubDivisions")
                        .HasForeignKey("DivisionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WePlayBall.Models.Team", b =>
                {
                    b.HasOne("WePlayBall.Models.SubDivision", "SubDivision")
                        .WithMany()
                        .HasForeignKey("SubDivisionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
