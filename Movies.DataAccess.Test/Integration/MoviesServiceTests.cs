

using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using Movies.DataAccess;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Movies.DataAccess.Services;
using Movies.DataAccess.Services.Models;

[TestFixture]
public class MoviesServiceTest
{
    private IMoviesService moviesService;

    [SetUp]
    public void SetUp() 
    {
        var serviceCollection = new ServiceCollection();

        ServiceCollectionRegistration.Setup(serviceCollection);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        moviesService = serviceProvider.GetRequiredService<IMoviesService>();
    }

    [Test]
    public async Task Test() 
    {
        var query = new MovieSearchQuery("Batman", null!);

        var results = await moviesService.SearchMovies(query);

        Assert.That(results.Length, Is.EqualTo(52));
    }
}