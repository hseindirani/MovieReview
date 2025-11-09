using Microsoft.EntityFrameworkCore;
using MovieReview.Models;

namespace MovieReview.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // DbSets (tables)
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieStudio> MovieStudios { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // -----------------
            // Movie ↔ Genre
            // -----------------
            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.MovieId);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenreId);

            // -----------------
            // Movie ↔ Studio
            // -----------------
            modelBuilder.Entity<MovieStudio>()
                .HasKey(ms => new { ms.MovieId, ms.StudioId });

            modelBuilder.Entity<MovieStudio>()
                .HasOne(ms => ms.Movie)
                .WithMany(m => m.MovieStudios)
                .HasForeignKey(ms => ms.MovieId);

            modelBuilder.Entity<MovieStudio>()
                .HasOne(ms => ms.Studio)
                .WithMany(s => s.MovieStudios)
                .HasForeignKey(ms => ms.StudioId);
        }
    }
}
