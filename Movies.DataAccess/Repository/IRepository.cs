using System.Linq.Expressions;

namespace Movies.DataAccess.Repository;

internal interface IRepository<TEntity> 
    where TEntity : class, new()
{
    IQueryable<TEntity> GetQuery();
}