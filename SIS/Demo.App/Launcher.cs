using Demo.App.Controllers;
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
    class Launcher
    {
        static void Main()
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            //[GET] Mappings

            serverRoutingTable
                .Add(HttpRequestMethod.Get, "/", request => new HomeController(request).Index(request));

            serverRoutingTable
                .Add(HttpRequestMethod.Get, "/users/login", request => new UsersController().Login(request));

            serverRoutingTable
                .Add(HttpRequestMethod.Get, "/users/register", request => new UsersController().Register(request));

            serverRoutingTable
                .Add(HttpRequestMethod.Get, "/users/logout", request => new UsersController().Logout(request));

            serverRoutingTable
                .Add(HttpRequestMethod.Get, "/home", request => new UsersController().Home(request));

            //[POST] Mappings

            serverRoutingTable
                .Add(HttpRequestMethod.Post, "/users/login", request => new UsersController().LoginConfirm(request));

            serverRoutingTable
                .Add(HttpRequestMethod.Post, "/users/register", request => new UsersController().RegisterConfirm(request));

            Server server = new Server(8000, serverRoutingTable);

            server.Run();
        }
    }
}
