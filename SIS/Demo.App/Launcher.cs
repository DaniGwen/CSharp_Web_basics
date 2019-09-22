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
                .Add(HttpRequestMethod.Get, "/", request => new HomeController(request)
                .Index(request));

            serverRoutingTable
                .Add(HttpRequestMethod.Get, "/users/login", httpRequest => new UsersController()
                .Login(httpRequest));

            serverRoutingTable
                .Add(HttpRequestMethod.Get, "/users/register", httpRequest => new UsersController()
                .Register(httpRequest));

            serverRoutingTable
                .Add(HttpRequestMethod.Get, "/users/logout", httpRequest => new UsersController()
                .Logout(httpRequest));

            serverRoutingTable
                .Add(HttpRequestMethod.Get, "/home", httpRequest => new HomeController(httpRequest)
                .Home(httpRequest));

            //[POST] Mappings

            serverRoutingTable
                .Add(HttpRequestMethod.Post, "/users/login", request => new UsersController()
                .LoginConfirm(request));

            serverRoutingTable
                .Add(HttpRequestMethod.Post, "/users/register", request => new UsersController()
                .RegisterConfirm(request));

            Server server = new Server(8000, serverRoutingTable);

            server.Run();
        }
    }
}
