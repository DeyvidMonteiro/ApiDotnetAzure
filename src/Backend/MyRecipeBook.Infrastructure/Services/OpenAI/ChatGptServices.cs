using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Services.OpenAI;

namespace MyRecipeBook.Infrastructure.Services.OpenAI;

public class ChatGptServices : IGenerateRecipeAI
{
    private const string CHAT_MODEL = "gpt-4o";
    public Task<GeneratedRecipeDto> Gerenate(IList<string> ingredients)
    {
        throw new NotImplementedException();
    }

    //sprint 4 aula 141
}
