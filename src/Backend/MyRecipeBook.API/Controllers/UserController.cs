using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.Users.Registrar;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.API.Controllers;


[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    public async Task <IActionResult> Register(
        [FromServices] IRegisterUseruseCase useCase,
        [FromBody] RequestRegisterUserJson request)
    {

        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}
