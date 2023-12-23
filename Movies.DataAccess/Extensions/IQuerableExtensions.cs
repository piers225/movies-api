
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
}
