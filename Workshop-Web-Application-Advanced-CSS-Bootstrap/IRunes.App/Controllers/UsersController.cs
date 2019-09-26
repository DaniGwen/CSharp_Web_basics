using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.App.Controllers
{
    public class UsersController : BaseController
    {
        public IHttpResponse Login(IHttpRequest request)
        {
            return this.View();
        }

        public IHttpResponse LoginConfirm(IHttpRequest request)
        {
            return null;
        }

        public IHttpResponse Register(IHttpRequest request)
        {
            return this.View();
        }

        public IHttpResponse RegisterConfirm(IHttpRequest request)
        {
            return null;
        }

        public IHttpResponse Logout(IHttpRequest request)
        {
            request.Session.ClearParameters();

            return this.Redirect("/");
        }
    }
}
