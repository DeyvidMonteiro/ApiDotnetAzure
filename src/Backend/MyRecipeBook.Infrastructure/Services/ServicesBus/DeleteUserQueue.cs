﻿using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Services.ServicesBus;

namespace MyRecipeBook.Infrastructure.Services.ServicesBus;

internal class DeleteUserQueue : IDeleteUserQueue
{
    public Task SendMessage(User user)
    {
        throw new NotImplementedException();
    }
}
