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

        }

        public IHttpResponse LoginConfirm(IHttpRequest httpRequest)
        {

        }

        public IHttpResponse Register(IHttpRequest httpRequest)
        {

        }

        public IHttpResponse RegisterConfirm(IHttpRequest httpRequest)
        {

        }

        public IHttpResponse Logout(IHttpRequest httpRequest)
        {
            if (this.IsLoggedIn())
            {
                httpRequest.Session.ClearParameter();

                return this.Redirect("/");
            }
        }
    }

}