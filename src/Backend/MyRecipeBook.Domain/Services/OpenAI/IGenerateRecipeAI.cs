using MyRecipeBook.Domain.Dtos;

namespace MyRecipeBook.Domain.Services.OpenAI;

public interface IGenerateRecipeAI
{
    Task<GeneratedRecipeDto> Gerenate(IList<string> ingredients);
}
