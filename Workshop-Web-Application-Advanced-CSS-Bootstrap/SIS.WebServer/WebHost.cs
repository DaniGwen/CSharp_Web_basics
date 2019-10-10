using SIS.WebServer.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.MvcFramework
{
    public static class WebHost
    {
        public static void Start(IMvcApplication application)
        {
            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            application.ConfigureServices();

            application.Configure(serverRoutingTable);

            Server server = new Server(8000, serverRoutingTable);
            server.Run();
        }
    }
}
