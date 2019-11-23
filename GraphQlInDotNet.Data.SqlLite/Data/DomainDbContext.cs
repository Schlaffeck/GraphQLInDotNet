using GraphQLInDotNet.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQlInDotNet.Data.EntityFramework.Data
{
    public class DomainDbContext : DbContext
    {
        public DomainDbContext(DbContextOptions<DomainDbContext> options)
        : base(options)
        {
        }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Artist> Artists{ get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArtistGenre>()
                .HasKey(ag => new { ag.ArtistId, ag.GenreId });
        }
    }
}
