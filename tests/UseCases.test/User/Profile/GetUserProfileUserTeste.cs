using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.UserBuilder;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.Users.Profile;
using Xunit;

namespace UseCases.test.User.Profile;

public class GetUserProfileUserTeste
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Name.Should().Be(user.Name);
        result.Email.Should().Be(user.Email);
    }

    private static GetUserProfileUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user)
    {
        var mapper = MapperBuilder.Build();
        var LoggedUser = LoggedUserBuilder.Build(user);

        return new GetUserProfileUseCase(LoggedUser, mapper);
    }
}
