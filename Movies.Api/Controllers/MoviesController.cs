using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Services;
using Movies.DataAccess.Services.Models;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMoviesService _movieService;

    public MoviesController(IMoviesService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public IActionResult SearchMovies([FromQuery] MovieSearchQuery movieSearchQuery)
    {
        if (ModelState.IsValid == false) 
        {
            return BadRequest(ModelState);
        }
        return Ok(_movieService.SearchMovies(movieSearchQuery));
    }
}