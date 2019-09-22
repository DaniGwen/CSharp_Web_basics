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

        protected Dictionary<string, object> viewData = new Dictionary<string, object>();

        protected bool IsLoggedIn()
        {
            return this.httpRequest.Session.ContainsParameter("username");
        }

        private string ParseTamplate(string viewContent)    
        {
            foreach (var param in viewData)
            {
                viewContent = viewContent.Replace($"@Model.{param.Key}", param.Value.ToString());
            }

            return viewContent;
        }
        public IHttpResponse View([CallerMemberName] string view = null)
        {
            string controllerName = this.GetType().Name.Replace("Controller", string.Empty);
            string viewName = view;

            string viewContent = File
                .ReadAllText(@"C:\Users\thinkpad\Documents\GitHub\C# Web basics\SIS\Demo.App\Views\" + controllerName + "\\" + viewName + ".html");

            viewContent = this.ParseTamplate(viewContent);

            HtmlResult htmlResult = new HtmlResult(viewContent, HttpResponseStatusCode.Ok);

            htmlResult.Cookies.AddCookie(new HttpCookie("lang", "en"));

            return htmlResult;
        }

        public IHttpResponse Redirect(string url)
        {
            return new RedirectResult(url);
        }
    }
}
