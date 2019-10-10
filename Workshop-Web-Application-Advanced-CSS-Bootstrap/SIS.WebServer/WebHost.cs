using SIS.HTTP.Enums;
using SIS.MvcFramework.Attributes;
using SIS.WebServer.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIS.MvcFramework
{
    public static class WebHost
    {
        public static void Start(IMvcApplication application)
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            AutoRegisterRoutes(application, serverRoutingTable);

            application.ConfigureServices();

            application.Configure(serverRoutingTable);

            Server server = new Server(8000, serverRoutingTable);
            server.Run();
        }

        private static void AutoRegisterRoutes(IMvcApplication application, IServerRoutingTable serverRoutingTable)
        {
            var controllers = application.GetType()
                 .Assembly
                 .GetTypes()
                 .Where(type => type.IsClass && !type.IsAbstract && typeof(Controller).IsAssignableFrom(type));
            //type.IsSubclassOf(typeof(Controller))

            foreach (var controller in controllers)
            {
                var actions = controller
                    .GetMethods(BindingFlags.Instance
                    | BindingFlags.Public
                    | BindingFlags.DeclaredOnly)
                    .Where(x => !x.IsSpecialName);

                foreach (var action in actions)
                {
                    var url = $"/{controller.Name.Replace("Controller", string.Empty)}/{action.Name}";
                    var attribute = action.CustomAttributes
                        .Where(x => x.AttributeType.IsSubclassOf(typeof(BaseHttpAttribute)));

                    var httpMethod = HttpRequestMethod.Get;
                    if (attribute?.AttributeType == typeof(HttpPostAttribute))
                    {
                        httpMethod = HttpRequestMethod.Post;
                    }
                    else if (attribute?.AttributeType == typeof(HttpDeleteAttribute))
                    {
                        httpMethod = HttpRequestMethod.Delete;
                    }

                    serverRoutingTable.Add()

                    Console.WriteLine(httpMethod + " " + url);
                }

            }
        }
    }
}
