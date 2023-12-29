

using Movies.DataAccess.Services.Enum;
using Movies.DataAccess.Services.Models;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

[TestFixture]
public class MovieSearchQueryTests
{
    [Test]
    public void ValidateMovieSearchQuery_ValidData_ShouldPassValidation()
    {
        var movieSearchQuery = new MovieSearchQuery(
            Title : "Inception",
            Genre : "Sci-Fi",
            Limit : 10,
            Page : 1,
            SortField : MovieSearchFields.Title,
            SortDirection : QueryOrderByEnum.Ascending
        );

        var validationResults = ValidateModel(movieSearchQuery);

        Assert.IsEmpty(validationResults);
    }

    private static System.Collections.Generic.List<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new System.Collections.Generic.List<ValidationResult>();
        var validationContext = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, validationContext, null);

        return validationResults;
    }
}
