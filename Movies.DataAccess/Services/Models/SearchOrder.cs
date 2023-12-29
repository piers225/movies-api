using Movies.DataAccess.Services.Enum;

namespace Movies.DataAccess.Services.Models;
public interface ISearchOrder
{
    public QueryOrderByEnum? SortDirection { get; }
}