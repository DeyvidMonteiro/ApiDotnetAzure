using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Extencions;
using MyRecipeBook.Domain.Services.OpenAI;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Recipe.Generate;

public class GenerateRecipeUseCase : IGenerateRecipeUseCase
{
    public GenerateRecipeUseCase()
    {
    }

    public async Task<ResponseGeneratedRecipeJson> Execute(RequestGenerateRecipeJson request)
    {
        //Validate(request);

        //var response = await _generator.Gerenate(request.Ingredients);

        //return new ResponseGeneratedRecipeJson
        //{
        //    Title = response.Title,
        //    Ingredients = response.Ingredients,
        //    CookingTime = (Communication.Enums.CookingTime)response.CookingTime,
        //    Instructions = response.Instructions.Select(c => new ResponseGeneratedInstructionJson
        //    {
        //        Step = c.Step,
        //        Text = c.Text,
        //    }).ToList(),
        //    Difficulty = Communication.Enums.Difficulty.Low
        //};
        throw new NotImplementedException();
    }

    private static void Validate(RequestGenerateRecipeJson request)
    {
        var result = new GenerateRecipeValidator().Validate(request);

        if (result.IsValid.IsFalse())
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
    }
}

