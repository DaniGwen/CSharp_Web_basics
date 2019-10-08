namespace IRunes.App
{
    public interface IMvcApplication
    {
        void Configure();

        void ConfigureServices(); // DI
    }
}