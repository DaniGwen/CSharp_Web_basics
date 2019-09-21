using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses.Contracts;
using SIS.Web.Server.Result;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Demo.App.Controllers
{
    public abstract class BaseController
    {
        protected IHttpRequest httpRequest { get; set; }

        protected bool IsLoggedIn()
        {
            return this.httpRequest.Session.ContainsParameter("username");
        }

        private string ParseTamplate(string viewContent)
        {
            if (this.IsLoggedIn())
            {
                return viewContent.Replace("@Model.HelloMessage", $"");
            }
        }
        public IHttpResponse View([CallerMemberName] string view = null)
        {
            string controllerName = this.GetType().Name.Replace("Controller", string.Empty);
            string viewName = view;

            string viewContent = File
                .ReadAllText(@"C:\Users\thinkpad\Documents\GitHub\C# Web basics\SIS\Demo.App\Views\" + controllerName + "\\" + viewName + ".html");

            HtmlResult htmlResult = new HtmlResult(viewContent, HttpResponseStatusCode.Ok);

            htmlResult.Cookies.AddCookie(new HttpCookie("lang", "en"));

            return htmlResult;
        }
    }
}
