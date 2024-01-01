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
    public async Task<IActionResult> GetMovies([FromQuery] MovieSearchQuery movieSearchQuery)
    {
        if (ModelState.IsValid == false) 
        {
            return BadRequest(ModelState);
        }
        return Ok(await _movieService.SearchMovies(movieSearchQuery));
    }
}