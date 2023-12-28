namespace Movies.DataAccess.DataContext.Repository;

internal class Repository<TEntity> : IRepository<TEntity> 
    where TEntity : class, new()
{

    private readonly MoviesDataContext dbContext;

    public Repository(MoviesDataContext dbContext) 
    {
        this.dbContext = dbContext;
    }

    public IQueryable<TEntity> GetQuery()
    {
        return this.dbContext.Set<TEntity>();
    }

}