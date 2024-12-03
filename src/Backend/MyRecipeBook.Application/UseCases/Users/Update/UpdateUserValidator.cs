using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Extencions;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.Users.Update;

public class UpdateUserValidator : AbstractValidator<ResquestUpdateUserJson>
{
    public UpdateUserValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceMessagesExceptions.NAME_EMPITY);
        RuleFor(request => request.Email).NotEmpty().WithMessage(ResourceMessagesExceptions.EMAIL_EMPITY);

        When(request => string.IsNullOrWhiteSpace(request.Email).IsFalse(), () => {
            RuleFor(request => request.Email).EmailAddress().WithMessage(ResourceMessagesExceptions.EMAIL_INVALID);
        });

    }
}
