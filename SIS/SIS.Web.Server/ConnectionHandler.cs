﻿using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.HTTP.Responses.Contracts;
using SIS.Web.Server.Routing;
using SIS.WebServer.Result;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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

        public async Task ProcessRequestAsync()
        {
            IHttpResponse httpResponse = null;

            try
            {
                IHttpRequest httpRequest = await this.ReadRequestAsync();

                if (httpRequest != null)
                {
                    Console.WriteLine($"Processing {httpRequest.RequestMethod} {httpRequest.Path}...");

                    httpResponse = this.HandleRequest(httpRequest);

                    this.PrepareResponse(httpResponse);
                }
            }
            catch (BadRequestException exception)
            {
                this.PrepareResponse(new TextResult(exception.ToString(), HttpResponseStatusCode.BadRequest));
            }
            catch (Exception e)
            {
                this.PrepareResponse(new TextResult(e.ToString(), HttpResponseStatusCode.InternalServerError));
            }

            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<IHttpRequest> ReadRequestAsync()
        {
            var result = new StringBuilder();
            var data = new ArraySegment<byte>(new byte[1024]);

            while (true)
            {
                int numberOfBytesRead = await this.client.ReceiveAsync(data, SocketFlags.None);

                if (numberOfBytesRead == 0)
                {
                    break;
                }

                var byteAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);
                result.Append(byteAsString);

                if (numberOfBytesRead < 1023)
                {
                    break;
                }
            }

            if (result.Length == 0)
            {
                return null;
            }

            return new HttpRequest(result.ToString());
        }

        private HttpResponse HandleRequest(IHttpRequest httpRequest)
        {
            if (!this.serverRoutingTable.Contains(httpRequest.RequestMethod, httpRequest.Path))
            {
                return new TextResult($"Route with method {httpRequest.RequestMethod} and path \"{httpRequest.Path}\" not found.", HttpResponseStatusCode.NotFound);
            }

            return (HttpResponse)this.serverRoutingTable.Get(httpRequest.RequestMethod, httpRequest.Path).Invoke(httpRequest);
        }

        private void PrepareResponse(IHttpResponse httpResponse)
        {
            byte[] byteSegments = httpResponse.GetBytes();

            this.client.Send(byteSegments, SocketFlags.None);
        }
    }
}
