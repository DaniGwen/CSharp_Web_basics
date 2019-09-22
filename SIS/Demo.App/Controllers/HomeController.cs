using Demo.App.Controllers;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.HTTP.Responses.Contracts;
using SIS.Web.Server.Result;
using System;
using System.Globalization;
using System.Text;

namespace Demo.App
{
    public class HomeController : BaseController
    {
        public HomeController(IHttpRequest request)
        {
            this.httpRequest = request;
        }
        public IHttpResponse Index(IHttpRequest request)
        {
            IHttpResponse newResponse = new HttpResponse();

            HttpCookie cookie = new HttpCookie("lang", "en");
            cookie.Delete();

            newResponse.AddCookie(cookie);

            return newResponse;
        }

        public IHttpResponse Home(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn())
            {
                return this.Redirect("/users/login");
            }

            return this.View();
        }
    }
}

