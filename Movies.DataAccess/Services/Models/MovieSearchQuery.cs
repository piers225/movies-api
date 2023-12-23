using Movies.DataAccess.Services.Enum;

namespace Movies.DataAccess.Services.Models;

public record MovieSearchQuery : ISearchOrder
{
    public string? Title { get; init; }
    public string? Genre { get; init; }
    public int Limit { get; init; }
    public int Page { get; init; }
    public string? SortBy { get; init; }
    public QueryOrderByEnum? OrderBy => SortBy?[..1] switch 
    {
        "-" => QueryOrderByEnum.Descending,
        "+" => QueryOrderByEnum.Ascending,
        _ => null!
    }
    public string? Field => SortBy?[1..]; 
}