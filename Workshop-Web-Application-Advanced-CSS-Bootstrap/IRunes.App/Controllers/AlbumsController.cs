using IRunes.App.Extensions;
using IRunes.App.Models;
using IRunes.App.ViewModels;
using IRunes.Services;
using IRunes.Services.Contracts;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using System.Collections.Generic;
using System.Linq;
using SIS.MvcFramework.Mapping;

namespace IRunes.App.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumService albumService;

        public AlbumsController()
        {
            this.albumService = new AlbumService();
        }

        [Authorize]
        public ActionResult All()
        {
            ICollection<Album> allAlbums = this.albumService.GetAllAlbums();

            if (allAlbums.Count == 0)
            {
                this.ViewData["Albums"] = "There are currently no albums.";
            }
            else
            {
                this.ViewData["Albums"] = string
                    .Join("<br />", allAlbums.Select(album => album.ToHtmlAll())
                    .ToList()
                    .To<AlbumViewModel>());
            }
            return this.View();
        }

        [Authorize]
        public ActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost(ActionName = "Create")]
        public ActionResult CreateConfirm()
        {
            string name = ((ISet<string>)this.Request.FormData["name"]).FirstOrDefault();
            string cover = ((ISet<string>)this.Request.FormData["cover"]).FirstOrDefault();

            var album = new Album
            {
                Cover = cover,
                Name = name,
                Price = 0M
            };

            this.albumService.CreateAlbum(album);

            return this.Redirect("/Albums/All");
        }

        [Authorize]
        public ActionResult Details()
        {
            string albumId = this.Request.QueryData["id"].ToString();

            Album albumFromDb = this.albumService.GetAlbumById(albumId);

            if (albumFromDb == null)
            {
                return this.Redirect("/Albums/All");
            }

            this.ViewData["Album"] = albumFromDb.ToHtmlDetails();

            return this.View();
        }
    }
}