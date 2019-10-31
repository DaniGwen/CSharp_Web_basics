namespace SIS.MvcFramework
{
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using SIS.Common;
    using SIS.HTTP.Common;
    using SIS.HTTP.Cookies;
    using SIS.HTTP.Enums;
    using SIS.HTTP.Exceptions;
    using SIS.HTTP.Requests;
    using SIS.HTTP.Responses;
    using SIS.HTTP.Sessions;
    using SIS.MvcFramework.Result;
    using SIS.MvcFramework.Sessions;
    using SIS.WebServer.Routing;

    public class ConnectionHandler
    {
        private readonly Socket client;
        private readonly IServerRoutingTable serverRoutingTable;
        private readonly IHttpSessionStorage httpSessionStorage;

        public ConnectionHandler(Socket client, IServerRoutingTable serverRoutingTable, IHttpSessionStorage httpSessionStorage)
        {
            client.ThrowIfNull(nameof(client));
            serverRoutingTable.ThrowIfNull(nameof(serverRoutingTable));
            httpSessionStorage.ThrowIfNull(nameof(httpSessionStorage));

            this.client = client;
            this.serverRoutingTable = serverRoutingTable;
            this.httpSessionStorage = httpSessionStorage;
        }

        private async Task<IHttpRequest> ReadRequestAsync()
        {
            // PARSE REQUEST FROM BYTE DATA
            var result = new StringBuilder();
            var data = new ArraySegment<byte>(new byte[1024]);

            while (true)
            {
                int numberOfBytesToRead = await client.ReceiveAsync(data, SocketFlags.None);

                if (numberOfBytesToRead == 0)
                {
                    break;
                }

                var bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesToRead);
                result.Append(bytesAsString);

                if (numberOfBytesToRead < 1023)
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

        private IHttpResponse ReturnIfResource(IHttpRequest httpRequest)
        {
            string folderPrefix = "/../../../../";
            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            string resourceFolder = "Resources";
            string requestedResource = httpRequest.Path;

            string fullPath = assemblyPath + folderPrefix + resourceFolder + requestedResource;

            if (File.Exists(fullPath))
            {
                byte[] content = File.ReadAllBytes(fullPath);

                return new InLineResourceResult(content, HttpResponseStatusCode.Found);
            }
            else
            {
                return new TextResult($"Route with method {httpRequest.RequestMethod} and path \"{httpRequest.Path}\" not found", HttpResponseStatusCode.NotFound);
            }
        }

        private IHttpResponse HandleRequest(IHttpRequest httpRequest)
        {
            // EXECUTE FUNCTION FOR CURRENT REQUEST -> RETURNS RESPONSE
            if (!serverRoutingTable.Contains(httpRequest.RequestMethod, httpRequest.Path))
            {
                return ReturnIfResource(httpRequest);
            }

            return serverRoutingTable
                .Get(httpRequest.RequestMethod, httpRequest.Path)
                .Invoke(httpRequest);
        }

        private string SetRequestSession(IHttpRequest httpRequest)
        {
            string sessionId = null;

            if (httpRequest.Cookies.ContainsCookie(HttpSessionStorage.SessionCookieKey))
            {
                var cookie = httpRequest.Cookies.GetCookie(HttpSessionStorage.SessionCookieKey);
                sessionId = cookie.Value;

                if (this.httpSessionStorage.ContainsSession(sessionId))
                {
                    httpRequest.Session = this.httpSessionStorage.GetSession(sessionId);
                }
            }

            return httpRequest.Session?.Id;
        }

        private void SetResponseSession(IHttpResponse httpResponse, string sessionId)
        {
            IHttpSession responseSession = this.httpSessionStorage.GetSession(sessionId);

            if (responseSession.IsNew)
            {
                responseSession.IsNew = false;
                httpResponse.Cookies
                    .AddCookie(new HttpCookie(HttpSessionStorage
                        .SessionCookieKey, responseSession.Id));
            }
        }

        private void PrepareResponse(IHttpResponse httpResponse)
        {
            // PREPARES RESPONSE -> MAPS IT TO BYTE DATA
            byte[] byteSegments = httpResponse.GetBytes();

            client.Send(byteSegments, SocketFlags.None);
        }

        public async Task ProcessRequestAsync()
        {
            IHttpResponse httpResponse = null;
            try
            {
                IHttpRequest httpRequest = await ReadRequestAsync();

                if (httpRequest != null)
                {
                    Console.WriteLine($"Processing: {httpRequest.RequestMethod} {httpRequest.Path}...");

                    string sessionId = SetRequestSession(httpRequest);

                    httpResponse = HandleRequest(httpRequest);

                    SetResponseSession(httpResponse, sessionId);
                }
            }
            catch (BadRequestException e)
            {
                httpResponse = new TextResult(e.Message, HttpResponseStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                httpResponse = new TextResult(e.Message, HttpResponseStatusCode.InternalServerError);
            }

            PrepareResponse(httpResponse);

            client.Shutdown(SocketShutdown.Both);
        }
    }
}
