using MyRecipeBook.Application.Services.Cryptography;

namespace CommonTestUtilities.Cryptograpy
{
    public class PasswordEncripterBuilder
    {
        public static PasswordEncripter Build()
        {
           return new PasswordEncripter("abc1234");
        }
    }
}
