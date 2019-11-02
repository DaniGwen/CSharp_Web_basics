using IRunes.App.Models;
using System.Linq;
using System.Net;

namespace IRunes.App.Extensions
{
    public static class EntityExtensions
    {
        public static string ToHtmlDetails(this Track track)
        {
            return $"<div class=\"track-details text-center\">" +
                   $"       <h3>Track Name: {WebUtility.UrlDecode(track.Name)}</h1>" +
                   $"       <h3>Track Price: ${track.Price:F2}</h1>" +
                   "<hr class=\"bg-success w-50\" style=\"height: 2px\" />" +
                   $"     <iframe src=\"{WebUtility.UrlDecode(track.Link)}\" class=\"w-50 mx-auto\" height=\"400\"></iframe>" +
                   $"</div>";
        }
    }
}
