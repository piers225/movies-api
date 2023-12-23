using Movies.DataAccess.Services.Models;

namespace Movies.DataAccess.Services;

public interface IMoviesService 
{
    Task<MovieQueryResult[]> SearchMovies(MovieSearchQuery movieSearchQuery);
}