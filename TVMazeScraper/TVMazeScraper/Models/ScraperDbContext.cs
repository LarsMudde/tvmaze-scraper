using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TVMazeScraper.Models
{
    public class ScraperDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ScraperDbContext(DbContextOptions<ScraperDbContext> options) : base(options) { }

        public ScraperDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=tvmazescraper-db;Trusted_Connection=True;ConnectRetryCount=0");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<TVShow> TVShows { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorTVShow> ActorsTVShows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorTVShow>()
               .HasKey(t => new { t.ActorId, t.TVShowId });

            modelBuilder.Entity<ActorTVShow>()
                .HasOne(pt => pt.Actor)
                .WithMany(p => p.TVShows)
                .HasForeignKey(pt => pt.ActorId);

            modelBuilder.Entity<ActorTVShow>()
                .HasOne(pt => pt.TVShow)
                .WithMany(t => t.Cast)
                .HasForeignKey(pt => pt.TVShowId);
        }
    }
}