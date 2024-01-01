

using Movies.DataAccess.Services.Enum;
using Movies.DataAccess.Services.Models;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

[TestFixture]
public class MovieSearchQueryTests
{
    [Test]
    public void Validate_MovieSearchQuery_With_Missing_Title_And_Genre_With_Limit_As_10_And_Page_As_1_With_Search_Field_Title_And_Sort_Ascending_Is_Valid()
    {
        var movieSearchQuery = new MovieSearchQuery(
            Title : "",
            Genre : "",
            Limit : 10,
            Page : 1,
            SortField : MovieSearchFields.Title,
            SortDirection : QueryOrderByEnum.Ascending
        );

        var validationResults = ValidateRecordModel(movieSearchQuery);

        Assert.IsEmpty(validationResults);
    }

    [Test]
    public void Validate_MovieSearchQuery_With_Missing_Title_And_Genre_With_Limit_As_10_And_Page_As_0_With_Search_Field_Title_And_Sort_Ascending_Is_Not_Valid()
    {
        var movieSearchQuery = new MovieSearchQuery(
            Title : "",
            Genre : "",
            Limit : 10,
            Page : 0,
            SortField : MovieSearchFields.Title,
            SortDirection : QueryOrderByEnum.Ascending
        );

        var validationResults = ValidateRecordModel(movieSearchQuery);

        Assert.That(validationResults.Count, Is.EqualTo(1));
    }

    [Test]
    public void Validate_MovieSearchQuery_With_Missing_Title_And_Genre_With_Limit_As_0_And_Page_As_1_With_Search_Field_Title_And_Sort_Ascending_Is_Not_Valid()
    {
        var movieSearchQuery = new MovieSearchQuery(
            Title : "",
            Genre : "",
            Limit : 0,
            Page : 1,
            SortField : MovieSearchFields.Title,
            SortDirection : QueryOrderByEnum.Ascending
        );

        var validationResults = ValidateRecordModel(movieSearchQuery);

        Assert.That(validationResults.Count, Is.EqualTo(1));
    }

    ///Unfortunately Validator.TryValidateObject has this problem
    ///https://github.com/dotnet/runtime/issues/64736 
    private List<ValidationResult?> ValidateRecordModel(object model)
    {
        var validationResults = new List<ValidationResult?>();
        var validationContext = new ValidationContext(model);

        var constructorInfo = model.GetType().GetConstructors().Single();

        foreach (var parameter in constructorInfo.GetParameters())
        {
            var value = model
                .GetType()
                .GetProperty(parameter.Name!)?
                .GetValue(model);
                
            var validationAttributes = parameter
                .GetCustomAttributes(true)
                .OfType<ValidationAttribute>();

            validationResults.AddRange(validationAttributes
                .Select(validationAttribute => validationAttribute.GetValidationResult(value, validationContext))
                .Where(w => w != ValidationResult.Success));
        }
    
        return validationResults;
    }
}
