using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses.Contracts;

namespace SIS.Web.Server.Routing
{
    public class ServerRoutingTable : IServerRoutingTable
    {

        private readonly Dictionary<HttpRequestMethod, Dictionary<string, Func<IHttpRequest, IHttpResponse>>> routes;

        public ServerRoutingTable()
        {
            this.routes = new Dictionary<HttpRequestMethod, Dictionary<string, Func<IHttpRequest, IHttpResponse>>>
            {
                [HttpRequestMethod.Get] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                [HttpRequestMethod.Post] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                [HttpRequestMethod.Delete] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                [HttpRequestMethod.Put] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>()
            };
        }
        public void Add(HttpRequestMethod requestMethod, string urlPath, Func<IHttpRequest, IHttpResponse> func)
        {
            throw new NotImplementedException();
        }

        public bool Contains(HttpRequestMethod requestMethod, string urlPath)
        {
            throw new NotImplementedException();
        }

        public Func<IHttpRequest, IHttpResponse> Get(HttpRequestMethod requestMethod, string urlPath)
        {
            throw new NotImplementedException();
        }
    }
}
