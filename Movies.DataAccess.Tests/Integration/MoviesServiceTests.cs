using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Movies.DataAccess.DataContext;
using Movies.DataAccess.Services;
using Movies.DataAccess.Services.Models;
using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Services.Enum;

namespace Movies.DataAccess.Test.Integration;

[TestFixture]
public class MoviesServiceTest
{
    private IMoviesService moviesService = null!;

    private MovieQueryResult batman = new(1, "Batman", "");
    private MovieQueryResult superman = new(2, "Superman", "");
    private MovieQueryResult findingNemo = new(3, "Finding Nemo", "");

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
    public async Task When_Title_Of_Man_Is_Queried_And_Order_By_Is_Set_To_Title_Asc_Then_We_Return_Batman_Then_Superman() 
    {
        var query = new MovieSearchQuery(Title : "man", Genre : null, Limit: null, Page : null, SortField : MovieSearchFields.Title, SortDirection : QueryOrderByEnum.Ascending);

        var results = await moviesService.SearchMovies(query);

        Assert.That(results.Length, Is.EqualTo(2));

        CollectionAssert.AreEqual(results, new [] { batman, superman});
    }

    [Test]
    public async Task When_Title_Of_Man_Is_Queried_And_Order_By_Is_Set_To_Title_Desc_Then_We_Return_Superman_Then_Batman() 
    {
        var query = new MovieSearchQuery(Title : "man", null, Limit: null, Page : null, SortField : MovieSearchFields.Title, SortDirection :  QueryOrderByEnum.Descending);

        var results = await moviesService.SearchMovies(query);

        Assert.That(results.Length, Is.EqualTo(2));

        CollectionAssert.AreEqual(results, new [] { superman, batman});
    }

    [Test]
    public async Task When_Genre_Adventure_Is_Queries_Then_Finding_Nemo_Is_Returned() 
    {
        var query = new MovieSearchQuery(Title : null, Genre : "Adventure", Limit: null, Page : null, SortField : null, SortDirection : null);

        var results = await moviesService.SearchMovies(query);

        Assert.That(results.Length, Is.EqualTo(1));

        CollectionAssert.AreEqual(results, new [] { findingNemo });
    }

    [Test]
    public async Task When_No_Sort_Order_Is_Given_Items_Are_Sorted_and_Paged_By_Id() 
    {
        var query = new MovieSearchQuery(Title : null, Genre : null, Limit: 1, Page : null, SortField : null, SortDirection : null);

        var resultPage1 = await moviesService.SearchMovies(query);
        var resultPage2 = await moviesService.SearchMovies(query with { Page = 2 });
        var resultPage3 = await moviesService.SearchMovies(query with { Page = 3 });

        CollectionAssert.AreEqual(resultPage1, new [] { batman });
        CollectionAssert.AreEqual(resultPage2, new [] { superman });
        CollectionAssert.AreEqual(resultPage3, new [] { findingNemo });
    }

    [Test]
    public async Task When_Title_Is_Bat_And_Genre_Is_Action_We_Return_Batman()
    {
        var query = new MovieSearchQuery(Title : "Bat", Genre : "Action", Limit: null, Page : null, SortField : null, SortDirection : null);

        var results = await moviesService.SearchMovies(query);

        Assert.That(results.Length, Is.EqualTo(1));
        CollectionAssert.AreEqual(results, new [] { batman });
    }

    [Test]
    public async Task When_Title_Is_Lower_Case_batma_And_Genre_Is_Lower_Case_action_We_Return_Batman()
    {
        var query = new MovieSearchQuery(Title : "batma", Genre : "action", Limit: null, Page : null, SortField : null, SortDirection : null);

        var results = await moviesService.SearchMovies(query);

        Assert.That(results.Length, Is.EqualTo(1));
        CollectionAssert.AreEqual(results, new [] { batman });
    }

    [Test]
    public async Task When_Sort_Direction_Is_Missing_An_Order_We_Use_Ascending()
    {
        var query = new MovieSearchQuery(Title : null, Genre : null, Limit: 1, Page : null, SortField : MovieSearchFields.Title, SortDirection : null);

        var resultPage1 = await moviesService.SearchMovies(query);

        CollectionAssert.AreEqual(resultPage1, new [] { batman });
    }


}