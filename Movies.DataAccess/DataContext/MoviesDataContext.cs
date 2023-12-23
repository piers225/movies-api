using Microsoft.EntityFrameworkCore;

namespace Movies.DataAccess.DataContext;

internal class MoviesDataContext : DbContext
{
    public MoviesDataContext(DbContextOptions<MoviesDataContext> options)
        : base(options)
    { }
    public DbSet<Genre> Genres { get; set; }

    public DbSet<Movie> Movies { get; set; }

    public DbSet<MovieGenreLink> MovieGenreLinks { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
       modelBuilder.Entity<MovieGenreLink>()
           .HasKey(link => new { link.MovieId, link.GenreId });
   }
}