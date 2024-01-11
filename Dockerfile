FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY Movies.Api/ ./Movies.Api/
COPY Movies.DataAccess/ ./Movies.DataAccess/
COPY Movies.DataAccess.Tests/ ./Movies.DataAccess.Tests/

RUN dotnet test Movies.DataAccess.Tests/Movies.DataAccess.Tests.csproj

RUN dotnet publish -c Release -o out Movies.Api/Movies.Api.csproj

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

RUN apt-get update && apt-get install -y curl

WORKDIR /app
COPY --from=build /app/out ./
COPY Movies.Client/ ./wwwRoot/
COPY Movies.DataAccess/Database/ ./database

ENTRYPOINT ["dotnet", "Movies.Api.dll"]
