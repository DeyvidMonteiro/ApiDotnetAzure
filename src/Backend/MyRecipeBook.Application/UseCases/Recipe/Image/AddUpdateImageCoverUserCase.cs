using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Http;
using MyRecipeBook.Domain.Extencions;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Recipe.Image;

public class AddUpdateImageCoverUserCase : IAddUpdateImageCoverUserCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IRecipeUpdateOnlyRepository _repository;
    private IUnitOfWork _unitOfWork;

    public AddUpdateImageCoverUserCase(ILoggedUser loggedUser, IRecipeUpdateOnlyRepository repository, IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long recipeId, IFormFile file)
    {
        var loggerUser = await _loggedUser.User();
        
        var recipe = await _repository.GetById(loggerUser, recipeId);

        if (recipe is null)
            throw new DirectoryNotFoundException(ResourceMessagesExceptions.RECIPE_NOT_FOUND);

        var fileStream = file.OpenReadStream();

        if (fileStream.Is<PortableNetworkGraphic>().IsFalse() && fileStream.Is<JointPhotographicExpertsGroup>().IsFalse())
        {
            throw new ErrorOnValidationException([ResourceMessagesExceptions.ONLY_IMAGES_ACCEPTED]);
        }
    }
}
