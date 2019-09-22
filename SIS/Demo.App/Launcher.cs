﻿using Demo.App.Controllers;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.Web.Server.Result;
using SIS.Web.Server.Routing;
using SIS.WebServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.App
{
    public class Launcher
    {
        public static void Main()
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            //[GET] Mappings

            serverRoutingTable.Add(HttpRequestMethod.Get, "/", request => new HomeController().Index(request));


            //[POST] Mappings


            Server server = new Server(8000, serverRoutingTable);

            server.Run();
        }
    }
}
