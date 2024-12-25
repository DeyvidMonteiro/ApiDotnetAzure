using System.Net;

namespace MyRecipeBook.Exceptions.ExceptionsBase;

public class UnauthorizedException : MyRecipeBookException
{
    public UnauthorizedException(string message) : base(message)
    {
    }

    public override IList<string> GetErrosMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}
