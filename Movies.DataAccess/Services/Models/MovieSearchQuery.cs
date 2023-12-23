using Movies.DataAccess.Services.Enum;

namespace Movies.DataAccess.Services.Models;

public record MovieSearchQuery : ISearchOrder
{
    public string? Title { get; init; }
    public string? Genre { get; init; }
    public int? Limit { get; init; }
    public int? Page { get; init; }
    public string? SortBy { get; init; }
    public QueryOrderByEnum? OrderBy => SortBy?.Split(':')[1] switch 
    {
        "asc" => QueryOrderByEnum.Descending,
        "desc" => QueryOrderByEnum.Ascending,
        _ => null!
    };
    public string? Field => SortBy?.Split(':')[0]; 

    public int PageOrDefault => Page.GetValueOrDefault(1);

    public int LimitOrDefault => Limit.GetValueOrDefault(200);
}