using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TVMazeScraper.Models
{
    public class TVShowContext : DbContext
    {
        public TVShowContext(DbContextOptions<TVShowContext> options) : base(options)
        {

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