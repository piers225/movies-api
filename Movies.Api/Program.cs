using Microsoft.AspNetCore.Diagnostics;
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
else 
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync("Unexpected Internal Server Error");
        });
    });
    app.UseHttpsRedirection();
}

app.MapGet("/api/movies", (IMoviesService movieService, [AsParameters] MovieSearchQuery movieSearchQuery) =>  movieService.SearchMovies(movieSearchQuery));

app.Run();