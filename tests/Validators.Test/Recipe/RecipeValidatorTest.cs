using CommonTestUtilities.Requests;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.Recipe;
using MyRecipeBook.Communication.Enums;
using MyRecipeBook.Exceptions;
using Xunit;
using System.Diagnostics.CodeAnalysis;


namespace Validators.Test.Recipe;

public class RecipeValidatorTest
{
    [Fact]
    public void Success()
    {
        var validate = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();

        var result = validate.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_InvalidCooking_Time()
    {
        var validate = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.CookingTime = (MyRecipeBook.Communication.Enums.CookingTime?)1000;

        var result = validate.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.COOKING_TIME_NOT_SUPPORTED));
    }

    [Fact]
    public void Error_Invalid_Difficulty()
    {
        var validate = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.Difficulty = (MyRecipeBook.Communication.Enums.Difficulty?)1000;

        var result = validate.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.DIFFICULTY_LEVEL_NOT_SUPPORTED));

    }

    [Theory]
    [InlineData(null)]
    [InlineData("      ")]
    [InlineData("")]
    [SuppressMessage("Usage", "xUnit1012:Null should only be used for nullable parameters", Justification = "<isso é um teste unit>")]
    public void Error_Empty_Title(string title)
    {
        var validate = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.Title = title;

        var result = validate.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.RECIPE_TITLE_EMPTY));
    }

    [Fact]
    public void Success_Cooking_Time_null()
    {
        var validate = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.CookingTime = null;

        var result = validate.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Success_Difficulty_null()
    {
        var validate = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.Difficulty = null;

        var result = validate.Validate(request);

        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void Success_DishTypes_Empty()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.DishTypes.Clear();

        var validate = new RecipeValidator();

        var result = validate.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Invalid_DishTypes()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.DishTypes.Add((DishType)1000);

        var validate = new RecipeValidator();

        var result = validate.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.DISH_TYPE_NOT_SUPPORTED));
    }

    [Fact]
    public void Error_Empty_Ingredients()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Ingredientes.Clear();

        var validate = new RecipeValidator();

        var result = validate.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.AT_LAST_ONE_INGREDIENT));
    }

    [Fact]
    public void Error_Empty_Instruction()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Instructions.Clear();

        var validate = new RecipeValidator();

        var result = validate.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.AT_LAST_ONE_INSTRUCTION));

    }

    [Theory]
    [InlineData("      ")]
    [InlineData("")]
    [InlineData(null)]
    [SuppressMessage("Usage", "xUnit1012:Null should only be used for nullable parameters", Justification = "<isso é um teste unit>")]

    public void Error_Empty_Value_Ingredients(string ingredients)
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Ingredientes.Add(ingredients);

        var validate = new RecipeValidator();

        var result = validate.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.INGREDIENT_EMPTY));
    }

    [Fact]
    public void Error_Same_Step_Instructions()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Instructions.First().Step = request.Instructions.Last().Step;

        var validate = new RecipeValidator();

        var result = validate.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.TWO_OR_MORE_INSTRUCTIONS_SAME_ORDERS));
    }

    [Fact]
    public void Error_Negative_Step_Instruction()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Instructions.First().Step = -1;

        var validate = new RecipeValidator();

        var result = validate.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.NON_NEGATIVE_INSTRUCTION_STEP));
    }

    [Theory]
    [InlineData("    ")]
    [InlineData("")]
    [InlineData(null)]
    [SuppressMessage("Usage", "xUnit1012:Null should only be used for nullable parameters", Justification = "<isso é um teste unit>")]
    public void Error_Empty_Value_Instructions(string instruction)
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Instructions.First().Text = instruction;

        var validate = new RecipeValidator();

        var result = validate.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.INSTRUCTION_EMPTY));
    }

    [Fact]
    public void Error_Instruction_Too_Long()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Instructions.First().Text = RequestStringGenerator.Paragraphs(minCharacters: 2001);

        var validator  = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.INSTRUCTION_EXCEEDS_LIMIT_CHARACTERS));
    }

}
