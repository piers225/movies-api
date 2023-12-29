

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
        // Arrange
        var movieSearchQuery = new MovieSearchQuery(
            Title : "Inception",
            Genre : "Sci-Fi",
            Limit : 10,
            Page : 1,
            SortField : MovieSearchFields.Title,
            SortDirection : QueryOrderByEnum.Ascending
        );

        // Act
        var validationResults = ValidateModel(movieSearchQuery);

        // Assert
        Assert.IsEmpty(validationResults);
    }

    [Test]
    public void ValidateMovieSearchQuery_InvalidData_ShouldFailValidation()
    {
        // Arrange
        var movieSearchQuery = new MovieSearchQuery
        (
            Title : null, 
            Genre : "Action",
            Limit : 0,    
            Page : -1,    
            SortField : (MovieSearchFields)10, 
            SortDirection : (QueryOrderByEnum)5   
        );

        // Act
        var validationResults = ValidateModel(movieSearchQuery);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(2, validationResults.Count); // Two validation errors expected

        });
    }

    private static System.Collections.Generic.List<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new System.Collections.Generic.List<ValidationResult>();
        var validationContext = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, validationContext, null);

        return validationResults;
    }
}
