using SIS.WebServer.Routing;

namespace IRunes.App
{
    public interface IMvcApplication
    {
        void Configure(ServerRoutingTable serverRoutingTable);

        void ConfigureServices(); // DI
    }
}