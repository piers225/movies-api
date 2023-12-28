using Movies.DataAccess.Services.Enum;
using System.ComponentModel.DataAnnotations;

namespace Movies.DataAccess.Services.Models;

public record class MovieSearchQuery(string? Title, string? Genre, int? Limit, int? Page, string? SortBy) : ISearchOrder
{
    public QueryOrderByEnum? OrderBy => SortBy?.Split(':').ElementAtOrDefault(1) switch 
    {
        "asc" => QueryOrderByEnum.Ascending,
        "desc" => QueryOrderByEnum.Descending,
        null => QueryOrderByEnum.Ascending,
        var other => throw new ArgumentException($"Unknown sort direction {other}")
    };
    public string? Field => SortBy?.Split(':')[0]; 

    public int PageOrDefault => Page.GetValueOrDefault(1);

    public int LimitOrDefault => Limit.GetValueOrDefault(200);
}