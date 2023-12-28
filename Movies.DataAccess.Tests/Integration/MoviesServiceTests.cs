using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using Movies.DataAccess;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Movies.DataAccess.DataContext;
using Movies.DataAccess.Services;
using Movies.DataAccess.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Movies.DataAccess.Test.Integration;

[TestFixture]
public class MoviesServiceTest
{
    private IMoviesService moviesService = null!;

    [OneTimeSetUp]
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
        context.MoviesGenresLink.Add(new MoviesGenresLink() { MovieId = 2, GenreId = 2});
        context.MoviesGenresLink.Add(new MoviesGenresLink() { MovieId = 3, GenreId = 1});

        context.SaveChanges();
    }

    [Test]
    public async Task When_Title_Of_Man_Is_Queried_And_Order_By_Is_Set_To_Title_Asc_Then_We_Return_Superman_Then_Batman() 
    {
        var query = new MovieSearchQuery(Title : "man", Genre : null, Limit: null, Page : null, SortBy : "Title:asc");

        var results = await moviesService.SearchMovies(query);

        Assert.That(results.Length, Is.EqualTo(2));

        CollectionAssert.AreEqual(results.Select(s => s.Id).ToArray(), new [] { 1, 2});
    }

    [Test]
    public async Task When_Title_Of_Man_Is_Queried_And_Order_By_Is_Set_To_Title_Desc_Then_We_Return_Batman_Then_Superman() 
    {
        var query = new MovieSearchQuery(Title : "man", null, Limit: null, Page : null, SortBy : "Title:desc");

        var results = await moviesService.SearchMovies(query);

        Assert.That(results.Length, Is.EqualTo(2));

        CollectionAssert.AreEqual(results.Select(s => s.Id).ToArray(), new [] { 2, 1});
    }

    [Test]
    public async Task When_Genre_Adventure_Is_Queries_Then_Finding_Nemo_Is_Returned() 
    {
        var query = new MovieSearchQuery(Title : null, Genre : "Adventure", Limit: null, Page : null, SortBy : null);

        var results = await moviesService.SearchMovies(query);

        Assert.That(results.Length, Is.EqualTo(1));

        CollectionAssert.AreEqual(results.Select(s => s.Id).ToArray(), new [] { 3 });
    }

    [Test]
    public async Task When_No_Sort_Order_Is_Given_Items_Are_Sorted_and_Paged_By_Id() 
    {
        var query = new MovieSearchQuery(Title : null, Genre : null, Limit: 1, Page : null, SortBy : null);

        var resultPage1 = await moviesService.SearchMovies(query);
        var resultPage2 = await moviesService.SearchMovies(query with { Page = 2 });
        var resultPage3 = await moviesService.SearchMovies(query with { Page = 3 });

        CollectionAssert.AreEqual(resultPage1.Select(s => s.Id).ToArray(), new [] { 1 });
        CollectionAssert.AreEqual(resultPage2.Select(s => s.Id).ToArray(), new [] { 2 });
        CollectionAssert.AreEqual(resultPage3.Select(s => s.Id).ToArray(), new [] { 3 });
    }


}