using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.HTTP.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Web.Server.Routing
{
    public interface IServerRoutingTable
    {
        void Add(HttpRequestMethod requestMethod, string urlPath, Func<IHttpRequest, IHttpResponse> func);
        bool Contains(HttpRequestMethod requestMethod, string urlPath);
        Func<IHttpRequest, IHttpResponse> Get(HttpRequestMethod requestMethod, string urlPath);
    }
}
