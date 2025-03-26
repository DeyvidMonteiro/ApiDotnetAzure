using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Domain.Services.ServicesBus;

public interface IDeleteUserQueue
{
    Task SendMessage(User user);
}
