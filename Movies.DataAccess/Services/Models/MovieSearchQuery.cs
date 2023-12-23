namespace Movies.DataAccess.Services.Models;

public record MovieSearchQuery 
{
    public string? Title { get; init; }
    public string? Genre { get; init; }
    public int Limit { get; init; } = 200;
    public int Page { get; init; } = 1;
    public SearchOrder? OrderBy { get; init; }
}