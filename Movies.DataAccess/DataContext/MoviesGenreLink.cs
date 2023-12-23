namespace Movies.DataAccess.DataContext;

internal class MovieGenreLink 
{
    public Movie Movie { get; set; } = default!;

    public Genre Genre { get; set; } = default!;

    public int MovieId { get; set; }
    
    public int GenreId { get; set; }
} 