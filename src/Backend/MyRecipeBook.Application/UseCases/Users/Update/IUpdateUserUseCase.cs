using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Application.UseCases.Users.Update;

public interface IUpdateUserUseCase
{
    public Task Execute(ResquestUpdateUserJson request);

    public Task Validate(ResquestUpdateUserJson request, string currentEmail);

}
