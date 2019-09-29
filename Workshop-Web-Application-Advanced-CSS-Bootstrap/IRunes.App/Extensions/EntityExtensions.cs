using IRunes.Models;
using System.Linq;

namespace IRunes.App.Extensions
{
    public static class EntityExtensions
    {
        public static string ToHtmlAll(this Album album)
        {
            return $"<div><a href=\"/Albums/Details?id={album.Id}\">{album.Name} </a></div>";
        }

        public static string ToHtmlDetails(this Album album)
        {
            return $"<div class=\"album-details\">" +
                "     <div class=\"album-data\">" +
                $"      <img src=\"{album.Cover}\">" +
                $"      <h1>Album Name: {album.Name}</h1>" +
                $"      <h1>Album Price: ${album.Price:F2}</h1>" +
                $"      <br />" +
                "   </div>" +
                "   <div class=\"album-tracks\">" +
                "       <h1>Tracks</h1>" +
                "       <hr style=\"height: 2px\" />" +
                "       <a href=\"/Tracks/Create\">Create Track</a>" +
                "       <hr style=\"height: 2px\" />" +
                "       <ul class=\"tracks-list\">" +
                $"        {album.Tracks.Select((track, index) => track.ToHtmlAll(index + 1))}" +
                "       </ul>" +
                "       <hr style=\"height: 2px\" />" +
                "    </div>" +
                "  </div>";

        }

        public static string ToHtmlAll(this Track track,int index)
        {
            return $"<li><a href=\"/Tracks/Details?id={track.Id}\">{index}. {track.Name}</li>";
        }

        public static string ToHtmlDetails(this Track track)
        {
            return null;
        }
    }
}
