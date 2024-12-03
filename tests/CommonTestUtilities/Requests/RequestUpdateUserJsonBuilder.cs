using Bogus;
using Bogus.Extensions.Italy;
using MyRecipeBook.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestUpdateUserJsonBuilder
{
    public static ResquestUpdateUserJson Build()
    {
        return new Faker<ResquestUpdateUserJson>()
            .RuleFor(user => user.Name, (f) => f.Person.FirstName)
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name));
            
    }
}
