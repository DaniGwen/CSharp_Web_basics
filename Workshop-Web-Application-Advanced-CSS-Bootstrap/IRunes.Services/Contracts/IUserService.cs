using IRunes.App.Models;
using System;

namespace Apps.IRunes.Services.Contracts
{
    public interface IUserService
    {
        User CreateUser(User user);

        User GetUserByUsernameAndPassword(string username, string password);
    }
}
