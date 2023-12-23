using System.Linq.Expressions;
using Movies.DataAccess.Services.Models;

namespace Movies.DataAccess.Extensions;

internal static class IQueryableExtensions
{
    public static int MAX_PAGE_LIMIT = 200;

    internal static IQueryable<T> Paginate<T>(this IQueryable<T> query, int page, int limit)
    {
        var pageSize = Math.Min(MAX_PAGE_LIMIT, limit);

        int skip = (page - 1) * pageSize;

        return query.Skip(skip).Take(pageSize);
    }

    internal static IQueryable<TSource> Sort<TSource, TKey>(this IQueryable<TSource> query, ISearchOrder searchOrder, Expression<Func<TSource, TKey>> keySelector) 
    {
        if (searchOrder.OrderBy == Services.Enum.QueryOrderByEnum.Ascending) 
        {
            return query.OrderBy(keySelector);
        }
        return query.OrderByDescending(keySelector);
    }
}
