namespace Movies.DataAccess.DataContext;

internal class MoviesGenresLink 
{
    public Movie Movie { get; set; } = default!;

    public Genre Genre { get; set; } = default!;

    public int MovieId { get; set; }
    
    public int GenreId { get; set; }
} 