using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.DataContext;

namespace Movies.DataAccess.Repository;

internal class Repository<TEntity> : IRepository<TEntity> 
    where TEntity : class, new()
{

    private readonly MoviesDataContext dbContext;

    public Repository(MoviesDataContext dbContext) 
    {
        this.dbContext = dbContext;
    }

    public async Task<TEntity?> FindOneOrNone(int id) 
    {
        return await this.dbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> FindOne(int id) 
    {
        return await this.dbContext.Set<TEntity>().FindAsync(id) 
            ?? throw new ArgumentException($"Id : {id}, not found for Table: {typeof(TEntity).Name}");
    }

    public Task<bool> Any(Expression<Func<TEntity, bool>> predicate) 
    {
        return this.dbContext.Set<TEntity>().AnyAsync(predicate);
    }

    public Task<TEntity[]> Where(Expression<Func<TEntity, bool>> predicate) 
    {
        return this.dbContext.Set<TEntity>().Where(predicate).ToArrayAsync();
    }

    public void Add(TEntity entity)
    {
        this.dbContext.Set<TEntity>().Add(entity);
    }

    public Task SaveChanges()
    {
        return this.dbContext.SaveChangesAsync();
    }
}