using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using SIS.Common;
using SIS.MvcFramework.Sessions;
using SIS.WebServer.Routing;

namespace SIS.MvcFramework
{
    public class Server
    {
        private const string LocalHostIpAddress = "127.0.0.1";

        private readonly int port;

        private readonly TcpListener tcpListener;

        private readonly IServerRoutingTable serverRoutingTable;

        private readonly IHttpSessionStorage httpSessionStorage;

        private bool isRunning;

        public Server(int port, IServerRoutingTable serverRoutingTable, IHttpSessionStorage httpSessionStorage)
        {
            this.serverRoutingTable.ThrowIfNull(nameof(this.serverRoutingTable));
            this.httpSessionStorage.ThrowIfNull(nameof(this.httpSessionStorage));

            this.port = port;
            this.serverRoutingTable = serverRoutingTable;
            tcpListener = new TcpListener(IPAddress.Parse(LocalHostIpAddress), port);
            this.httpSessionStorage = httpSessionStorage;
        }

        private async Task ListenAsync(Socket client)
        {
            var connectionHandler = new ConnectionHandler(client, this.serverRoutingTable, this.httpSessionStorage);
            await connectionHandler.ProcessRequestAsync();
        }

        public void Run()
        {
            tcpListener.Start();
            isRunning = true;

            Console.WriteLine($"Server started at http://{LocalHostIpAddress}:{port}");

            while (isRunning)
            {
                var client = tcpListener.AcceptSocketAsync().GetAwaiter().GetResult();
                Task.Run(() => ListenAsync(client));
            }
        }
    }
}
