using Movies.DataAccess.Repository;
using Movies.DataAccess.DataContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Services;

namespace Movies.DataAccess;

public static class ServiceCollectionRegistration 
{ 
    public static void Setup(IServiceCollection serviceCollection) 
    {
        serviceCollection.AddDbContext<MoviesDataContext>(options => options.UseSqlite("Data Source=/app/Movies.DataAccess/Database/movies.db"));
        serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        serviceCollection.AddScoped(typeof(IMoviesService), typeof(MoviesService));
    }
}