using IRunes.App.Models;
using System;

namespace IRunes.Services.Contracts
{
    public interface IUserService
    {
        User CreateUser(User user);

        User GetUserByUsernameAndPassword(string username, string password);
    }
}
