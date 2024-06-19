using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.Users.Registrar
{
    internal class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesExceptions.NAME_EMPITY);
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesExceptions.NAME_EMPITY);
            RuleFor(user => user.Email).EmailAddress();
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6);
        }

    }
}
