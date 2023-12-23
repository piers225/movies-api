using Movies.Api.Binders;
using Movies.DataAccess;
using Movies.DataAccess.Services;
using Movies.DataAccess.Services.Models;

var builder = WebApplication.CreateBuilder(args);

ServiceCollectionRegistration.Setup(builder.Services);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(options =>
    {
        options.ModelBinderProviders.Insert(0, new MovieSearchQueryBinderProvider());
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/api/movies", (IMoviesService movieService, MovieSearchQuery movieSearchQuery) => movieService.SearchMovies(movieSearchQuery));

app.Run();