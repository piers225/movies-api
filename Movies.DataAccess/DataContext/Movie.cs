namespace Movies.DataAccess.DataContext;
internal class Movie
{
    public int Id { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Title { get; set; } = null!;
    public string Overview { get; set; } = null!;
    public decimal Popularity { get; set; }
    public int VoteCount { get; set; }
    public decimal VoteAverage { get; set; }
    public string OriginalLanguage { get; set; } = null!;
    public string PosterUrl { get; set; }  = null!;
    public ICollection<MoviesGenresLink> MoviesGenresLinks { get; set; } = null!;
}