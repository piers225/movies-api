using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.DataAccess.DataContext;

internal class Genre 
{
    public int Id { get; set; }

    [Column("Genre")]
    public string GenreName { get; set; } = default!;

    public ICollection<MoviesGenresLink> MoviesGenresLinks { get; set; } = default!;
}