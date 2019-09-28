using IRunes.Models;

namespace IRunes.App.Extensions
{
    public static class EntityExtensions
    {
        public static string ToHtmlAll(this Album album)
        {
            return $"<a href=\"/Albums/Details?id={album.Id}\">{album.Name} </a>";
        }

        public static string ToHtmlAll(this Track track)
        {
            return null;
        }

        public static string ToHtmlDetails(this Album album)
        {
            return null;
        }

        public static string ToHtmlDetails(this Track track)
        {
            return null;
        }
    }
}
