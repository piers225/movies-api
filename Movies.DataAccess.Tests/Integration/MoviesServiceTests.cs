

using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using Movies.DataAccess;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Movies.DataAccess.DataContext;
using Movies.DataAccess.Services;
using Movies.DataAccess.Services.Models;
using Microsoft.EntityFrameworkCore;

[TestFixture]
public class MoviesServiceTest
{
    private IMoviesService moviesService;

    [SetUp]
    public void SetUp() 
    {
        var serviceCollection = new ServiceCollection();

        ServiceCollectionRegistration.SetupServices(serviceCollection);

        serviceCollection.AddDbContext<MoviesDataContext>(options => options.UseInMemoryDatabase("MyDatabase"));

        var serviceProvider = serviceCollection.BuildServiceProvider();

        moviesService = serviceProvider.GetRequiredService<IMoviesService>();

        var context = serviceProvider.GetRequiredService<MoviesDataContext>();

        context.Genres.Add(new Genre() { Id = 1, GenreName = "Adventure"});
        context.Genres.Add(new Genre() { Id = 2, GenreName = "Action"});

        context.Movies.Add(new Movie() { 
            Title = "Batman",
            Id = 1,
            OriginalLanguage = "en",
            Overview = "",
            PosterUrl = ""
        });
        context.Movies.Add(new Movie() { 
            Title = "Superman",
            Id = 2,
            OriginalLanguage = "en",
            Overview = "",
            PosterUrl = ""
        });
        context.Movies.Add(new Movie() { 
            Title = "Finding Nemo",
            Id = 3,
            OriginalLanguage = "en",
            Overview = "",
            PosterUrl = ""
        });

        context.MoviesGenresLink.Add(new MoviesGenresLink() { MovieId = 1, GenreId = 2});

        context.SaveChanges();
    }

    [Test]
    public async Task Test() 
    {
        var query = new MovieSearchQuery {
            Page = 1,
            Limit = 200,
            Title = "man",
            SortBy = "Title:asc"
        };

        var results = await moviesService.SearchMovies(query);

        Assert.That(results.Length, Is.EqualTo(2));

        CollectionAssert.AreEqual(results.Select(s => s.Id).ToArray(), new [] { 2, 1});
    }
}