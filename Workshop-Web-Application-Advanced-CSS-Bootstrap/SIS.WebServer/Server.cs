using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using SIS.HTTP.Common;
using SIS.WebServer.Routing;

namespace SIS.MvcFramework
{
    public class Server
    {
        private const string LocalHostIpAddress = "127.0.0.1";

        private readonly int port;

        private readonly TcpListener tcpListener;

        private IServerRoutingTable serverRoutingTable;

        private bool isRunning;

        public Server(int port, IServerRoutingTable serverRoutingTable)
        {
            CoreValidator.ThrowIfNull(serverRoutingTable, nameof(serverRoutingTable));

            this.port = port;
            this.serverRoutingTable = serverRoutingTable;

            tcpListener = new TcpListener(IPAddress.Parse(LocalHostIpAddress), port);
        }

        private async Task ListenAsync(Socket client)
        {
            var connectionHandler = new ConnectionHandler(client, serverRoutingTable);
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
