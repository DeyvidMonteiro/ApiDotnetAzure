using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.API.Attributes;
using MyRecipeBook.Application.UseCases.Users.ChangePassword;
using MyRecipeBook.Application.UseCases.Users.Delete;
using MyRecipeBook.Application.UseCases.Users.Profile;
using MyRecipeBook.Application.UseCases.Users.Registrar;
using MyRecipeBook.Application.UseCases.Users.Update;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.API.Controllers;

public class UserController : MyRecipeBookBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUseruseCase useCase,
        [FromBody] RequestRegisterUserJson request)
    {

        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }


    [HttpGet]
    [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
    [AuthenticatedUser]
    public async Task<IActionResult> GetUserprofiler([FromServices] IGetUserProfilerUseCase useCase)
    {
        var result = await useCase.Execute();

        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser]
    public async Task <IActionResult> Updade(
        [FromServices] IUpdateUserUseCase useCase,
        [FromBody] ResquestUpdateUserJson resquest)
    {
        await useCase.Execute(resquest);

        return NoContent();
    }

    [HttpPut("change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser]
    public async Task<IActionResult> ChangePassword(
        [FromServices] IChangePasswordUseCase useCase,
        [FromBody] RequestChangePasswordJson request)
    {
        await useCase.Execute(request);

        return NoContent();
    }

    [HttpDelete]
    [Obsolete("Este endpoint está em desenvolvimento e não deve ser usado ainda.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [AuthenticatedUser]
    public async Task<IActionResult> Delete([FromServices] IRequestDeleteUserUseCase useCase)
    {
        await useCase.Execute();

        return NoContent();
    }



}
