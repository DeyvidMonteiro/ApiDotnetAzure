using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Application.UseCases.Users.ChangePassword;

public interface IChangePasswordUseCase
{
    public Task Execute(RequestChangePasswordJson request);
}
