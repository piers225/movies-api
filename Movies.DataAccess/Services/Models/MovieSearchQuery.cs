namespace Movies.DataAccess.Services.Models;

public record MovieSearchQuery 
{
    public string? Title { get; init; }
    public string? Genre { get; init; }
    public int Limit { get; init; }
    public int Page { get; init; }
    public SearchOrder? SortBy { get; init; }
}