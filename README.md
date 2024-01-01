# Movie Search Web API

## Description

This Web API is for searching for movies from a sqlite database. 

## Movie Database API - Overview

The Movies API uses the route '/api/movies'. The following parameters allows you to search, sort and paginate.

- `Title`: The title of the movie.
- `Genre`: The genre of the movie. ( Action, Adventure, Animation, Comedy, Crime, Documentary, Drama, Family, Fantasy, History, Horror, Music, Mystery, Romance, Science Fiction, Thriller, TV Movie, War, Western )
- `Limit`: The maximum number of results to be returned (positive integer). The default is 200.
- `Page`: The page number for paginated results (positive integer ). The default is 1.
- `SortField`: The field by which the results should be sorted ( Title or ReleaseDate ). The default is Title.
- `SortDirection`: The direction of sorting ( Ascending or Descending ). The default is Ascending.

### Validation 

Any parameters that don't pass the above validation should return a 400 response with an explanation. 

### Error Logging

When running locally any error message will display in the browser.
When running in production a GUID will show with a message. The error can be looked up in the log using the GUID. 

## Movie Database API - Usage Examples

The following are examples of calls we can make,

### Search by Title and Genre:

Endpoint: `/api/movies?title=Inception&genre=Action`

This example allows you to search for movies with the title "Inception" in the Action genre.

### Search with Pagination:

Endpoint: `/api/movies?limit=10&page=1`

Use pagination to limit the number of results per page (10 in this case) and navigate through pages.

### Sort by Title in Ascending Order:

Endpoint: `/api/movies?sortField=Title&sortDirection=ascending`

Sort the movies by title in ascending order.

### Sort by Release Date in Descending Order:

Endpoint: `/api/movies?sortField=ReleaseDate&sortDirection=descending`

Sort the movies by release date in descending order.

## Running the API Locally

This requires the .NET CORE 8.0 SDK to be installed locally.

For macOS/Linux:
```bash
dotnet run --project ./Movies.Api/Movies.Api.csproj
```

For Windows
```command line
dotnet run --project .\Movies.Api\Movies.Api.csproj
```

Then you should be able to open [http://localhost:5001](http://localhost:5001)

## Running the API from a Dev Container

Please follow the setup instructions from [Visual Code Documentation](https://code.visualstudio.com/docs/devcontainers/containers). You should then be able to start the project inside a dev container. Then from the terminal,

For Linux:
```bash
dotnet run --project ./Movies.Api/Movies.Api.csproj
```

Then you should be able to run open [http://localhost:5001](http://localhost:5001)

## Docker build

This requires docker to be installed 

```command line
docker build -t movies-api .

docker-compose up -d 
```

Then you should be able to run open [http://localhost](http://localhost)
