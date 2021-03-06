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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TVShow>()
                        .HasMany(s => s.Cast)
                        .WithMany(a => a.TVShows)
                        .UsingEntity<Dictionary<long, object>>(
                            x => x.HasOne<Actor>().WithMany().HasForeignKey("ActorId").OnDelete(DeleteBehavior.Cascade),
                            x => x.HasOne<TVShow>().WithMany().HasForeignKey("TVShowId").OnDelete(DeleteBehavior.Cascade));
        }
    }
}