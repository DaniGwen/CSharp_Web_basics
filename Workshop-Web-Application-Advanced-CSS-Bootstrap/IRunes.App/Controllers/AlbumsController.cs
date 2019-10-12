using IRunes.App.Data;
using IRunes.App.Extensions;
using IRunes.App.Models;
using Microsoft.EntityFrameworkCore;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using System.Collections.Generic;
using System.Linq;

namespace IRunes.App.Controllers
{
    public class AlbumsController : Controller
    {
        [Authorize]
        public ActionResult All()
        {
            using (var context = new RunesDbContext())
            {
                ICollection<Album> allAlbums = context.Albums.ToList();

                if (allAlbums.Count == 0)
                {
                    this.ViewData["Albums"] = "There are currently no albums.";
                }
                else
                {
                    this.ViewData["Albums"] = string
                        .Join("<br />", allAlbums.Select(album => album.ToHtmlAll()).ToList());
                }

                return this.View();
            }
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
            if (!this.IsLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            using (var context = new RunesDbContext())
            {
                string name = ((ISet<string>)this.Request.FormData["name"]).FirstOrDefault();
                string cover = ((ISet<string>)this.Request.FormData["cover"]).FirstOrDefault();

                var album = new Album
                {
                    Cover = cover,
                    Name = name,
                    Price = 0M
                };

                context.Albums.Add(album);
                context.SaveChanges();
            }

            return this.View("/All");
        }

        [Authorize]
        public ActionResult Details()
        {
            string albumId = this.Request.QueryData["id"].ToString();

            using (var context = new RunesDbContext())
            {
                Album albumFromDb = context.Albums
                    .Include(album => album.Tracks)
                    .SingleOrDefault(album => album.Id == albumId);

                if (albumFromDb == null)
                {
                    return this.Redirect("/Albums/All");
                }

                this.ViewData["Album"] = albumFromDb.ToHtmlDetails();

                return this.View();
            }
        }
    }
}
