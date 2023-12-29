using Movies.DataAccess.Services.Enum;
using System.ComponentModel.DataAnnotations;

namespace Movies.DataAccess.Services.Models;

public record class MovieSearchQuery(
    string? Title, 
    string? Genre, 
    [Range(1, int.MaxValue, ErrorMessage = "The {0} must be a positive number.")] int? Limit, 
    [Range(1, int.MaxValue, ErrorMessage = "The {0} must be a positive number.")] int? Page, 
    [EnumDataType(typeof(MovieSearchFields))] MovieSearchFields? SortField,
    [EnumDataType(typeof(QueryOrderByEnum))] QueryOrderByEnum? SortDirection
    ) : ISearchOrder
{

    public int PageOrDefault => Page.GetValueOrDefault(1);

    public int LimitOrDefault => Limit.GetValueOrDefault(200);
} 