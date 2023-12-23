
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.DataContext;
using Movies.DataAccess.DataContext.Repository;
using Movies.DataAccess.Extensions;
using Movies.DataAccess.Services.Models;

namespace Movies.DataAccess.Services;

internal class MoviesService : IMoviesService
{
    private readonly IRepository<Movie> movieRepository;

    public MoviesService(IRepository<Movie> movieRepository)
    {
        this.movieRepository = movieRepository;
    }

    private Expression<Func<Movie, dynamic>> GetSortExpression(ISearchOrder searchOrder)
    {
        return searchOrder.Field switch
        {
            "Title" => movie => movie.Title, 
            "ReleaseDate" => movie => movie.ReleaseDate,
            _ => throw new ArgumentException($"Invalid Sort Field: {searchOrder.Field}"),
        };
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

        if (movieSearchQuery.SortBy is not null) 
        {
            query = query.Sort(movieSearchQuery, GetSortExpression(movieSearchQuery));
        }
        
        return await query
            .Paginate(movieSearchQuery.PageOrDefault, movieSearchQuery.LimitOrDefault)
            .Select(movie => new MovieQueryResult(movie.Id, movie.Title, movie.PosterUrl))
            .ToArrayAsync();
    }
}