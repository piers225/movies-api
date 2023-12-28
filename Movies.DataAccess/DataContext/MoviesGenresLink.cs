namespace Movies.DataAccess.DataContext;

internal class MoviesGenresLink 
{
    public Movie Movie { get; set; } = null!;

    public Genre Genre { get; set; } = null!;

    public int MovieId { get; set; }
    
    public int GenreId { get; set; }
} 