using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.DataAccess.DataContext;

internal class Genre 
{
    [Column("Genre")]
    public string GenreName { get; set; } = default!;

    public required ICollection<MovieGenreLink> MoviesGenresLinks { get; set; } = default!;
}