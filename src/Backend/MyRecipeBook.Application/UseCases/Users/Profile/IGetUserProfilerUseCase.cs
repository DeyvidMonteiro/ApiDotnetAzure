using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCases.Users.Profile;

public interface IGetUserProfilerUseCase
{
    public Task<ResponseUserProfileJson> Execute();

}
