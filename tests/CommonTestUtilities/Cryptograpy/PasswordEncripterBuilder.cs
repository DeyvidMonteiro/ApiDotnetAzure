using MyRecipeBook.Domain.Security.Criptography;
using MyRecipeBook.Infrastructure.Security.Cryptography;

namespace CommonTestUtilities.Cryptograpy
{
    public class PasswordEncripterBuilder
    {
        public static IPasswordEncripter Build()
        {
            return new Sha512Encripter("abc1234");
        }
    }
}
