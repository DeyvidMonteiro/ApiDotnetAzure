using FluentValidation;
using MyRecipeBook.Application.SharedValidator;
using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Application.UseCases.Users.ChangePassword;

public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordJson>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<RequestChangePasswordJson>());
    }
}
