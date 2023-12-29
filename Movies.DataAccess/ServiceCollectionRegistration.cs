using Movies.DataAccess.DataContext.Repository;
using Movies.DataAccess.DataContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Services;
using Microsoft.Extensions.Configuration;

namespace Movies.DataAccess;

public static class ServiceCollectionRegistration 
{ 
    public static void Setup(IServiceCollection serviceCollection, IConfiguration configuration) 
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), configuration["DATABASE_PATH"] ?? "../Movies.DataAccess/Database", "movies.db");
        serviceCollection.AddDbContext<MoviesDataContext>(options => options.UseSqlite($"Data Source=/{path}"));
        SetupServices(serviceCollection);
    }

    internal static void SetupServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        serviceCollection.AddScoped(typeof(IMoviesService), typeof(MoviesService));
    }
}