﻿using IRunes.App.Data;
using IRunes.App.Models;
using IRunes.Services.Contracts;
using System.Linq;

namespace IRunes.Services
{
    public class UserService : IUserService
    {
        private RunesDbContext context;

        public UserService()
        {
            this.context = new RunesDbContext();
        }

        public User CreateUser(User user)
        {
            user = this.context.Users.Add(user).Entity;
            this.context.SaveChanges();

            return user;
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            return this.context.Users
                 .SingleOrDefault(user => user.Username == username || user.Email == username
                                                                    && user.Password == password);
        }
    }
}
