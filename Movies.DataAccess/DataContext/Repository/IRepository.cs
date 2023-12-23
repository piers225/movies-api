namespace Movies.DataAccess.DataContext.Repository;

internal interface IRepository<TEntity> 
    where TEntity : class, new()
{
    IQueryable<TEntity> GetQuery();
}