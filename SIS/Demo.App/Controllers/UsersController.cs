using Demo.Data;
using Demo.Models;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.App.Controllers
{
    public class UsersController : BaseController
    {
        public IHttpResponse Login(IHttpRequest httpRequest)
        {
            return this.View();
        }

        public IHttpResponse LoginConfirm(IHttpRequest httpRequest)
        {
            using (var dbContext = new DemoDbContext())
            {
                string username = httpRequest.FormData["username"].ToString();
                string password = httpRequest.FormData["password"].ToString();
                    
                User user = dbContext.Users.
            }
        }

        public IHttpResponse Register(IHttpRequest httpRequest)
        {
            return this.View();
        }

        public IHttpResponse RegisterConfirm(IHttpRequest httpRequest)
        {
            using (var dbContext = new DemoDbContext())
            {
                string username = httpRequest.FormData["username"].ToString();
                string password = httpRequest.FormData["password"].ToString();
                string confirmPassword = httpRequest.FormData["confirmPassword"].ToString();

                if (password != confirmPassword)
                {
                    return this.Redirect("/register");
                }

                var user = new User
                {
                    Username = username,
                    Password = password
                };

                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }

            return this.Redirect("/users/login");
        }

        public IHttpResponse Logout(IHttpRequest httpRequest)
        {
            httpRequest.Session.ClearParameter();

            return this.Redirect("/");
        }
    }

}