using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.UserBuilder;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.Dashboard;
using Xunit;

namespace UseCases.test.Dashboard;

public class GetDashboardUserCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, _) = UserBuilder.Build();
        var recipe = RecipeBuilder.Collection(user);

        var useCase = CreatedUseCase(user, recipe);

        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Recipes.Should()
            .HaveCountGreaterThan(0)
            .And.OnlyHaveUniqueItems(recipe => recipe.Id)
            .And.AllSatisfy(recipe =>
            {
                recipe.Id.Should().NotBeNullOrWhiteSpace();
                recipe.Title.Should().NotBeNullOrWhiteSpace();
                recipe.AmountIngredients.Should().BeGreaterThan(0);
            });

    }

    private static GetDashboardUseCase CreatedUseCase(
        MyRecipeBook.Domain.Entities.User user,
        IList<MyRecipeBook.Domain.Entities.Recipe> recipes)
    {
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var repository = new RecipeReadOnlyRepositoryBuilder().GetForDashboard(user, recipes).Build();

        return new GetDashboardUseCase(repository, mapper, loggedUser);
    }
}
