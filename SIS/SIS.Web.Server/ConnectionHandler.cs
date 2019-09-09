using SIS.HTTP.Common;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.HTTP.Responses.Contracts;
using SIS.Web.Server.Routing;
using System;
using System.Net.Sockets;

namespace SIS.WebServer
{
    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IServerRoutingTable serverRoutingTable;

        public ConnectionHandler(Socket client, IServerRoutingTable serverRoutingTable)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(serverRoutingTable, nameof(serverRoutingTable));

            this.client = client;
            this.serverRoutingTable = serverRoutingTable;
        }

        public void ProcessRequest()
        {
            try
            {
                var httpRequest = this.ReadRequest();

                if (httpRequest != null)
                {
                    Console.WriteLine($"Processing {httpRequest.RequestMethod} {httpRequest.Path}...");

                    var httpResponse = this.HandleRequest(httpRequest);

                    this.PrepareResponse(httpResponse);
                }
            }
            catch ()
            {

                throw;
            }
        }

        private HttpRequest ReadRequest()
        {

        }

        private HttpResponse HandleRequest(IHttpRequest httpRequest)
        {

        }

        private void PrepareResponse(IHttpResponse httpResponse)
        {

        }
    }
}
