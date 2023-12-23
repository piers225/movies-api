using Microsoft.EntityFrameworkCore;

namespace Movies.DataAccess.DataContext;

internal class MoviesDataContext(DbContextOptions<MoviesDataContext> options) : DbContext(options)
{
    public DbSet<Genre> Genres { get; set; } = null!;

    public DbSet<Movie> Movies { get; set; } = null!;

    public DbSet<MovieGenreLink> MovieGenreLinks { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
       modelBuilder.Entity<MovieGenreLink>()
           .HasKey(link => new { link.MovieId, link.GenreId });
   }
}