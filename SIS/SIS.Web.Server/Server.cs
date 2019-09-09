using SIS.Web.Server.Routing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SIS.WebServer
{
    public class Server
    {
        private const string LocalhostIpAddress = "127.0.0.1";

        private readonly int port;

        private readonly TcpListener tcpListener;

        private readonly IServerRoutingTable routingTable;

        private bool isRunning;

        public Server(int port, IServerRoutingTable routingTable)
        {
            this.port = port;
            this.routingTable = routingTable;
            this.tcpListener = new TcpListener(IPAddress.Parse(LocalhostIpAddress), port);

            while (this.isRunning)
            {
                Console.WriteLine($"waiting for client...");

                var client = this.tcpListener.AcceptSocket();

                this.Listen(client);
            }
        }

        public void Run()
        {
            this.tcpListener.Start();
            this.isRunning = true;

            Console.WriteLine($"Server started at {LocalhostIpAddress}:{this.port}");
        }

        public async Task Listen(Socket client)
        {
            var connectionHandler = new ConnectionHandler(client, this.routingTable);
            connectionHandler.ProcessRequest();
        }
    }
}
