using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WePlayBall.Models;
using Microsoft.EntityFrameworkCore;

namespace WePlayBall.Data
{
    // ReSharper disable once InconsistentNaming
    public class WPBDataContext : DbContext
    {
        public WPBDataContext(DbContextOptions<WPBDataContext> options) : base(options)
        {
        }

        /// <summary>
        /// When the database is created, EF creates tables that have names the same as the DbSet
        /// property names. Property names for collections are typically plural.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataSourceFixture>().ToTable("DataSourceFixture");
            modelBuilder.Entity<DataSourceRanking>().ToTable("DataSourceRanking");
            modelBuilder.Entity<DataSourceResult>().ToTable("DataSourceResult");
            modelBuilder.Entity<Division>().ToTable("Division");
            modelBuilder.Entity<Fixture>().ToTable("Fixture");
            modelBuilder.Entity<GameResult>().ToTable("GameResult");
            modelBuilder.Entity<TeamStat>().ToTable("TeamStat");
            modelBuilder.Entity<SubDivision>().ToTable("SubDivision");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<ReportTracker>().ToTable("ReportTracker");

            modelBuilder.Entity<Team>()
                .HasIndex(x => x.TeamCode)
                .IsUnique();


            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();
            
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Username)
                .IsUnique();
        }


        public DbSet<DataSourceFixture> DataSourceFixtures { get; set; }
        public DbSet<DataSourceRanking> DataSourceRankings { get; set; }
        public DbSet<DataSourceResult> DataSourceResults { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<GameResult> GameResults { get; set; }
        public DbSet<TeamStat> TeamStats { get; set; }
        public DbSet<SubDivision> SubDivisions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<ReportTracker>ReportTracking { get; set; }
    }
}
