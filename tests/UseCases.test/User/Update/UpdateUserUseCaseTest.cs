﻿using CommonTestUtilities.Requests;
using CommonTestUtilities.UserBuilder;
using FluentAssertions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Xunit;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Application.UseCases.Users.Update;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.LoggedUser;
using MyRecipeBook.Domain.Extencions;

namespace UseCases.test.User.Update;

public class UpdateUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, _) = UserBuilder.Build();

        var request = RequestUpdateUserJsonBuilder.Build();

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();
        user.Name.Should().Be(request.Name);
        user.Email.Should().Be(request.Email);
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        (var user, _) = UserBuilder.Build();

        var request = RequestUpdateUserJsonBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => { await useCase.Execute(request); };

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.GetErrosMessages().Count == 1 &&
                e.GetErrosMessages().Contains(ResourceMessagesExceptions.NAME_EMPITY));

        user.Name.Should().NotBe(request.Name);
        user.Email.Should().NotBe(request.Email);
    }

    [Fact]
    public async Task Error_Email_Already_Registered()
    {
        (var user, _) = UserBuilder.Build();

        var request = RequestUpdateUserJsonBuilder.Build();

        var useCase = CreateUseCase(user, request.Email);

        Func<Task> act = async () => { await useCase.Execute(request); };

        await act.Should().ThrowAsync<ErrorOnValidationException>()
            .Where(e => e.GetErrosMessages().Count == 1 &&
                e.GetErrosMessages().Contains(ResourceMessagesExceptions.EMAIL_ALREADY_REGISTERED));

        user.Name.Should().NotBe(request.Name);
        user.Email.Should().NotBe(request.Email);
    }

    private static UpdateUserUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user, string? email = null)
    {
        var unitOfWok = UnitOfWorkBuilder.Build();

        var userUpdateRepository = new UserUpdateOnlyRepositoryBuilder().GetById(user).Build();

        var loggedUser = LoggedUserBuilder.Build(user);

        var userReadOnlyRepoitoryBuilder = new UserReadOnlyRepositoryBuilder();

        if (string.IsNullOrEmpty(email).IsFalse())
            userReadOnlyRepoitoryBuilder.ExistActiveUserWithEmail(email!);

        return new UpdateUserUseCase(loggedUser, userUpdateRepository, userReadOnlyRepoitoryBuilder.Build(), unitOfWok);


    }


}
