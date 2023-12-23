using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Movies.DataAccess.Services.Models;

namespace Movies.Api.Binders;

internal class MovieSearchQueryBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        // Retrieve the values from the query string
        var titleValue = bindingContext.ValueProvider.GetValue("Title").FirstValue;
        var genreValue = bindingContext.ValueProvider.GetValue("Genre").FirstValue;
        var limitValue = bindingContext.ValueProvider.GetValue("Limit").FirstValue;
        var pageValue = bindingContext.ValueProvider.GetValue("Page").FirstValue;
        //var sortByValue = bindingContext.ValueProvider.GetValue("SortBy").FirstValue;

        // Create a new instance of the MovieSearchQuery record
        var movieSearchQuery = new MovieSearchQuery
        {
            Title = titleValue,
            Genre = genreValue,
            Limit = string.IsNullOrEmpty(limitValue) ? 200 : int.Parse(limitValue),
            Page = string.IsNullOrEmpty(pageValue) ? 1 : int.Parse(pageValue),
            //SortBy = string.IsNullOrEmpty(sortByValue) ? null : Enum.Parse<SearchOrder>(sortByValue, true)
        };

        // Set the result to succeed with the created MovieSearchQuery
        bindingContext.Result = ModelBindingResult.Success(movieSearchQuery);
        return Task.CompletedTask;
    }
}

internal class MovieSearchQueryBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(MovieSearchQuery))
        {
            return new BinderTypeModelBinder(typeof(MovieSearchQuery));
        }

        return null;
    }
}
