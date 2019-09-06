using SIS.HTTP.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Extensions
{
    public static class HttpResponseStatusCodeExtension
    {
        public static string GetResponseCode(this HttpResponseStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpResponseStatusCode.Ok:
                    return "200 Ok";
                case HttpResponseStatusCode.Created:
                    return "201 Created";
                case HttpResponseStatusCode.Found:
                    return "302 Found";
                case HttpResponseStatusCode.SeeOther:
                    return "303 See Other";
                case HttpResponseStatusCode.BadRequest:
                    return "400 Bad Request";
                case HttpResponseStatusCode.Unauthorized:
                    return "401 Unauthorized";
                case HttpResponseStatusCode.Forbidden:
                    return "403 Forbidden";
                case HttpResponseStatusCode.NotFound:
                    return "404 Not Found";
                case HttpResponseStatusCode.InternalServerError:
                    return "500 Internal Server Error";
            }
            return null;
        }
    }
}
