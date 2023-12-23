using Movies.DataAccess.Services.Enum;

namespace Movies.DataAccess.Services.Models;
public record SearchOrder
{
    public QueryOrderByEnum OrderBy { get; init; }
    public string? Field { get; init; }
}