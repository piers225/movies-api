using Movies.DataAccess;
using Movies.DataAccess.Services;
using Movies.DataAccess.Services.Models;

var builder = WebApplication.CreateBuilder(args);

ServiceCollectionRegistration.Setup(builder.Services);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/movies", (IMoviesService movieService, [AsParameters] MovieSearchQuery movieSearchQuery) =>  movieService.SearchMovies(movieSearchQuery));

app.Run();