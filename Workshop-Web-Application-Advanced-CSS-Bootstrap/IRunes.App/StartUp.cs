﻿using IRunes.App.Controllers;
using IRunes.App.Data;
using SIS.HTTP.Enums;
using SIS.MvcFramework;
using SIS.WebServer;
using SIS.WebServer.Result;
using SIS.WebServer.Routing;

namespace IRunes.App
{
    public class StartUp : IMvcApplication
    {
        public void Configure(IServerRoutingTable serverRoutingTable)
        {
            using (var context = new RunesDbContext())
            {
                context.Database.EnsureCreated();
            }

            serverRoutingTable.Add(HttpRequestMethod.Get, "/Info/About", request => new InfoController().About(request));

            #region Home Routes

            serverRoutingTable.Add(HttpRequestMethod.Get, "/", request => new RedirectResult("/Home/Index"));

            serverRoutingTable.Add(HttpRequestMethod.Get, "/Home/Index", request => new HomeController().Index(request));

            #endregion

            #region Users routes

            serverRoutingTable.Add(HttpRequestMethod.Get, "/Users/Login", request => new UsersController()
            .Login(request));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/Users/Logout", request => new UsersController()
            .Logout(request));
            serverRoutingTable.Add(HttpRequestMethod.Post, "/Users/Login", request => new UsersController()
            .LoginConfirm(request));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/Users/Register", request => new UsersController()
            .Register(request));
            serverRoutingTable.Add(HttpRequestMethod.Post, "/Users/Register", request => new UsersController()
            .RegisterConfirm(request));

            #endregion

            #region Albums routes

            serverRoutingTable.Add(HttpRequestMethod.Get, "/Albums/All", request => new AlbumsController()
            .All(request));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/Albums/Create", request => new AlbumsController()
            .Create(request));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/Albums/Details", request => new AlbumsController()
            .Details(request));
            serverRoutingTable.Add(HttpRequestMethod.Post, "/Albums/Create", request => new AlbumsController().CreateConfirm(request));

            #endregion

            #region Tracks Routes

            serverRoutingTable.Add(HttpRequestMethod.Get, "/Tracks/Create", request => new TracksController()
            .Create(request));
            serverRoutingTable.Add(HttpRequestMethod.Post, "/Tracks/Create", request => new TracksController()
            .CreateConfirm(request));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/Tracks/Details", request => new TracksController()
            .Details(request));

            #endregion
        
        }

        public void ConfigureServices()
        {
          
        }
    }
}