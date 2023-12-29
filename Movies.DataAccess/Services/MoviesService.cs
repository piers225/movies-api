
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.DataContext;
using Movies.DataAccess.DataContext.Repository;
using Movies.DataAccess.Extensions;
using Movies.DataAccess.Services.Enum;
using Movies.DataAccess.Services.Models;

namespace Movies.DataAccess.Services;

internal class MoviesService : IMoviesService
{
    private readonly IRepository<Movie> movieRepository;

    public MoviesService(IRepository<Movie> movieRepository)
    {
        this.movieRepository = movieRepository;
    }

    private Expression<Func<Movie, dynamic>> GetSortExpression(MovieSearchQuery searchQuery)
    {
        return searchQuery.SortField switch
        {
            MovieSearchFields.Title => movie => movie.Title, 
            MovieSearchFields.ReleaseDate => movie => movie.ReleaseDate,
            _ => movie => movie.Title
        };
    }

    public async Task<MovieQueryResult[]> SearchMovies(MovieSearchQuery movieSearchQuery)
    {
        var query = movieRepository.GetQuery();

        if (movieSearchQuery.Title is not null) 
        {
            query = query.Where(movie => EF.Functions.Like(movie.Title, $"%{movieSearchQuery.Title}%"));
        }

        if (movieSearchQuery.Genre is not null) 
        {
            query = query.Where(movie => movie.MoviesGenresLinks.Any(link => link.Genre.GenreName.ToUpper() == movieSearchQuery.Genre.ToUpper()));
        }
        
        return await query
            .Sort(movieSearchQuery, GetSortExpression(movieSearchQuery))
            .Paginate(movieSearchQuery.PageOrDefault, movieSearchQuery.LimitOrDefault)
            .Select(movie => new MovieQueryResult(movie.Id, movie.Title, movie.PosterUrl))
            .ToArrayAsync();
    }
}