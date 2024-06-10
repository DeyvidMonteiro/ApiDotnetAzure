using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.API.Controllers;


[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJason),StatusCodes.Status201Created)]
    public IActionResult Register(RequestRegisterUserJson request)
    {
        return Created();
    }
}
