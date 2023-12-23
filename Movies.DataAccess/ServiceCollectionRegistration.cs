using Movies.DataAccess.Repository;
using Movies.DataAccess.DataContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Movies.DataAccess;

public static class ServiceCollectionRegistration { 

    public static void Setup(IServiceCollection serviceCollection) 
    {
        serviceCollection.AddDbContext<MoviesDataContext>(options => options.UseSqlite("Data Source=/Database/movies.db"));
        ServiceCollectionRegistration.SetupWithoutContext(serviceCollection);
    }

    internal static void SetupWithoutContext(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}