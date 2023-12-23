namespace Movies.DataAccess.DataContext;
internal class Movie
{
    public int Id { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Title { get; set; } = default!;
    public string Overview { get; set; } = default!;
    public decimal Popularity { get; set; }
    public int VoteCount { get; set; }
    public decimal VoteAverage { get; set; }
    public string OriginalLanguage { get; set; } = default!;
    public string PosterUrl { get; set; }  = default!;
    public ICollection<MovieGenreLink> MoviesGenresLinks { get; set; } = default!;
}