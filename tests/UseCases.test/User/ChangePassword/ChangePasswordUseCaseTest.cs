﻿using CommonTestUtilities.Cryptograpy;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.UserBuilder;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.Users.ChangePassword;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Xunit;

namespace UseCases.test.User.ChangePassword;

public class ChangePasswordUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var password) = UserBuilder.Build();

        var request = RequestChangePasswordJsonBuilder.Build();
        request.Password = password;

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();

        var passwordEncripter = PasswordEncripterBuilder.Build();

        user.Password.Should().Be(passwordEncripter.Encrypt(request.NewPassword));
    }

    [Fact]
    public async Task Error_NewPassword_Empty()
    {
        (var user, var password) = UserBuilder.Build();

        var request = new RequestChangePasswordJson
        {
            Password = password,
            NewPassword = string.Empty
        };

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => { await useCase.Execute(request); };

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.GetErrosMessages().Count == 1 &&
                e.GetErrosMessages().Contains(ResourceMessagesExceptions.PASSWORD_EMPITYB));

        var passwordEncripter = PasswordEncripterBuilder.Build();

        user.Password.Should().Be(passwordEncripter.Encrypt(password));
    }

    [Fact]
    public async Task Error_CurrentPassword_Different()
    {
        (var user, var password) = UserBuilder.Build();

        var request = RequestChangePasswordJsonBuilder.Build();

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => { await useCase.Execute(request); };

        await act.Should().ThrowAsync<ErrorOnValidationException>()
            .Where(e => e.GetErrosMessages().Count == 1 &&
                e.GetErrosMessages().Contains(ResourceMessagesExceptions.PASSWORD_DIFFERENT_CURRENT_PASSWORD));

        var passwordEncripter = PasswordEncripterBuilder.Build();

        user.Password.Should().Be(passwordEncripter.Encrypt(password));
    }


    private static ChangePasswordUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var userUpdateRepository =  new UserUpdateOnlyRepositoryBuilder().GetById(user).Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var passwordEncripter = PasswordEncripterBuilder.Build();

        return new ChangePasswordUseCase(loggedUser, userUpdateRepository, unitOfWork, passwordEncripter);
    }

}
