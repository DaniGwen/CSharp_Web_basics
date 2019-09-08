using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses.Contracts;

namespace SIS.Web.Server.Routing
{
    public class ServerRoutingTable : IServerRoutingTable
    {

        private readonly Dictionary<HttpRequestMethod, Dictionary<string, Func<IHttpRequest, IHttpResponse>>> routingTable;

        public ServerRoutingTable()
        {
            this.routingTable = new Dictionary<HttpRequestMethod, Dictionary<string, Func<IHttpRequest, IHttpResponse>>>
            {
                [HttpRequestMethod.Get] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                [HttpRequestMethod.Post] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                [HttpRequestMethod.Delete] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                [HttpRequestMethod.Put] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>()
            };
        }
        public void Add(HttpRequestMethod requestMethod, string urlPath, Func<IHttpRequest, IHttpResponse> func)
        {
            CoreValidator.ThrowIfNull(requestMethod, nameof(requestMethod));
            CoreValidator.ThrowIfNullOrEmpty(urlPath, nameof(urlPath));
            CoreValidator.ThrowIfNull(func, nameof(func));

            this.routingTable[requestMethod].Add(urlPath, func);
        }

        public bool Contains(HttpRequestMethod requestMethod, string urlPath)
        {
            CoreValidator.ThrowIfNull(requestMethod, nameof(requestMethod));
            CoreValidator.ThrowIfNullOrEmpty(urlPath, nameof(urlPath));

            return this.routingTable.ContainsKey(requestMethod) && this.routingTable[requestMethod].ContainsKey(urlPath);
        }

        public Func<IHttpRequest, IHttpResponse> Get(HttpRequestMethod requestMethod, string urlPath)
        {
            CoreValidator.ThrowIfNull(requestMethod, nameof(requestMethod));
            CoreValidator.ThrowIfNullOrEmpty(urlPath, nameof(urlPath));

            return this.routingTable[requestMethod][urlPath];
        }
    }
}
