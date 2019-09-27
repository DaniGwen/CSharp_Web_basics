namespace IRunes.App
{
    using SIS.HTTP.Enums;
    using Data;
    using SIS.WebServer.Routing;
    using SIS.WebServer;
    using SIS.WebServer.Result;
    using IRunes.App.Controllers;

    public class Launcher
    {
        public static void Main(string[] args)
        {
            using (var context = new RunesDbContext())
            {
                context.Database.EnsureCreated();
            }

            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();
            Configure(serverRoutingTable);

            Server server = new Server(8000, serverRoutingTable);
            server.Run();
        }

        private static void Configure(ServerRoutingTable serverRoutingTable)
        {
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
            .RegisterConfirm (request));
             
            #endregion


        }


    }
}
