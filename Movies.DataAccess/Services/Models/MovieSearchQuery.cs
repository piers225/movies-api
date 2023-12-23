using Movies.DataAccess.Services.Enum;

namespace Movies.DataAccess.Services.Models;

public record MovieSearchQuery : ISearchOrder
{
    public string? Title { get; init; }
    public string? Genre { get; init; }
    public int Limit { get; init; }
    public int Page { get; init; }
    public string? SortBy { get; init; }
    public QueryOrderByEnum? OrderBy => SortBy?.Substring(0, 1) == "-" ? QueryOrderByEnum.Descending : QueryOrderByEnum.Ascending;
    public string? Field => SortBy?.Substring(1); 
}