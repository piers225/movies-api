
using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.DataContext;
using Movies.DataAccess.Repository;
using Movies.DataAccess.Services.Models;

namespace Movies.DataAccess.Services;

internal class MoviesService : IMoviesService
{
    private readonly IRepository<Movie> movieRepository;

    public MoviesService(IRepository<Movie> movieRepository)
    {
        this.movieRepository = movieRepository;
    }

    public async Task<MovieQueryResult[]> SearchMovies(MovieSearchQuery movieSearchQuery)
    {
        var query = movieRepository.GetQuery();

        if (movieSearchQuery.Title is not null) 
        {
            query = query.Where(movie => movie.Title.Contains(movieSearchQuery.Title));
        }

        if (movieSearchQuery.Genre is not null) 
        {
            query = query.Where(movie => movie.MoviesGenresLinks.Any(link => link.Genre.GenreName == movieSearchQuery.Genre));
        }

        return await query
            .Take(100)
            .Select(movie => new MovieQueryResult(movie.Id, movie.Title))
            .ToArrayAsync();
    }
}