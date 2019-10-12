﻿using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.MvcFramework.Extensions;
using SIS.MvcFramework.Identity;
using SIS.MvcFramework.Result;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace SIS.MvcFramework
{
    public abstract class Controller
    {
        public object Htmlresult { get; private set; }

        protected Controller()
        {
            ViewData = new Dictionary<string, object>();    
        }

        protected Dictionary<string, object> ViewData;

        protected Principal User => (Principal)this.Request.Session.GetParameter("principal");

        public IHttpRequest Request { get; set; }

        protected bool IsLoggedIn()
        {
            return this.User != null;
        }

        protected void SignIn(string id, string username, string email)
        {
         this.Request.Session.AddParameter("principal", new Principal
         {
             Id = id,
             Username = username,
             Email = email
         });
       
        }

        protected void SignOut()
        {
            this.Request.Session.ClearParameters();
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
        protected ActionResult View([CallerMemberName] string view = null)
        {
            string controllerName = GetType().Name.Replace("Controller", string.Empty);
            string viewName = view;
            string viewContent = System.IO.File.ReadAllText("Views/" + controllerName + "/" + viewName + ".html");
            viewContent = ParseTemplate(viewContent);
            HtmlResult htmlResult = new HtmlResult(viewContent, HttpResponseStatusCode.Ok);

            return htmlResult;
        }

        protected ActionResult Redirect(string url)
        {
            return new RedirectResult(url);
        }

        protected ActionResult Xml(object obj)
        {
            return new XmlResult(obj.ToXml());
        }

        protected ActionResult Jason(object obj)
        {
            return new JsonResult(obj.ToJson());
        }

        protected ActionResult File(byte[] content)
        {
            return new FileResult(content);
        }

        protected ActionResult NotFound(string message = "")
        {
            return new NotFoundResult(message);
        }
    }
}
