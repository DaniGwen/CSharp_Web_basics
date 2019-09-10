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
    class Program
    {
        public IHttpResponse Index(HttpRequest request)
        {
            string content = "<h1>Hi world!</h1>";

            return new HtmlResult(content, HttpResponseStatusCode.Ok);
        }
    }
}

