namespace Movies.DataAccess.Services.Models;
public record class MovieQueryResult(int Id, string Title, DateTime ReleaseDate, string PosterUrl);