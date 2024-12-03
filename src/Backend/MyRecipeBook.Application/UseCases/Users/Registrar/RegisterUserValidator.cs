using FluentValidation;
using MyRecipeBook.Application.SharedValidator;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Extencions;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.Users.Registrar
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesExceptions.NAME_EMPITY);
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesExceptions.EMAIL_EMPITY);
            RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
            When(user => string.IsNullOrEmpty(user.Email).IsFalse(), () =>
            {
                RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesExceptions.EMAIL_INVALID);
            });
        }

    }
}
