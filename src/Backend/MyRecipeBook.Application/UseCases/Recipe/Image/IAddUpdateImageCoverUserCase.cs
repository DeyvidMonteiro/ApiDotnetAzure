using Microsoft.AspNetCore.Http;

namespace MyRecipeBook.Application.UseCases.Recipe.Image;

public interface IAddUpdateImageCoverUserCase
{
    Task Execute (long recipeId, IFormFile file);
}
