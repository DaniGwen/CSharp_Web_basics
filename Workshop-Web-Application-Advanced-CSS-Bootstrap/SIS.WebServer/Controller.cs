
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework.Result;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace SIS.MvcFramework
{
    public abstract class Controller
    {
        protected Dictionary<string, object> ViewData;

        public object Htmlresult { get; private set; }

        protected Controller()
        {
            ViewData = new Dictionary<string, object>();
        }

        protected bool IsLoggedIn(IHttpRequest httpRequest)
        {
            return httpRequest.Session.ContainsParameter("username");
        }

        protected void SignIn(IHttpRequest httpRequest, string id, string username,string email)
        {
            httpRequest.Session.AddParameter("username", username);
            httpRequest.Session.AddParameter("email", email);
            httpRequest.Session.AddParameter("id", id);
        }

        protected void SignOut(IHttpRequest httpRequest)
        {
            httpRequest.Session.ClearParameters();
        }

        private string ParseTemplate(string viewContent)
        {
            foreach (var param in ViewData)
            {
                viewContent = viewContent.Replace($"@Model.{param.Key}", param.Value.ToString());
            }

            return viewContent;
        }
                                        
                                    //Takes the name of the calling method    
        protected IHttpResponse View([CallerMemberName] string view = null)
        {
            string controllerName = GetType().Name.Replace("Controller", string.Empty);

            string viewName = view;

            string viewContent = File.ReadAllText("Views/" + controllerName + "/" + viewName + ".html");

            viewContent = ParseTemplate(viewContent);

            HtmlResult htmlResult = new HtmlResult(viewContent, HttpResponseStatusCode.Ok);

            return htmlResult;
        }

        protected IHttpResponse Redirect(string url)
        {
            return new RedirectResult(url);
        }
    }
}
