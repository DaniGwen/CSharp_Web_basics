using IRunes.App.Data;
using IRunes.App.Extensions;
using IRunes.App.Models;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunes.App.Controllers
{
    public class TracksController : Controller
    {
        [Authorize]
        public ActionResult Create()
        {
            string albumId = this.Request.QueryData["albumId"].ToString();
            this.ViewData["AlbumId"] = albumId;

            return this.View();
        }

        [Authorize]
        [HttpPost(ActionName = "Create")]
        public ActionResult CreateConfirm()
        {
            string albumId = this.Request.QueryData["albumId"].ToString();

            using (var context = new RunesDbContext())
            {
                Album albumFromDb = context.Albums.SingleOrDefault(album => album.Id == albumId);

                if (albumFromDb == null)
                {
                    return this.Redirect("/Albums/All");
                }

                string name = ((ISet<string>)this.Request.FormData["name"]).FirstOrDefault();
                string link = ((ISet<string>)this.Request.FormData["link"]).FirstOrDefault();
                string price = ((ISet<string>)this.Request.FormData["price"]).FirstOrDefault();

                Track trackForDb = new Track
                {
                    Name = name,
                    Link = link,
                    Price = decimal.Parse(price)
                };

                albumFromDb.Tracks.Add(trackForDb);

                albumFromDb.Price = (albumFromDb.Tracks.Select(track => track.Price).Sum() * 87) / 100;

                context.Update(albumFromDb);
                context.SaveChanges();
            }

            return this.Redirect($"/Albums/Details?id={albumId}");
        }

        [Authorize]
        public ActionResult Details()
        {
            string albumId = this.Request.QueryData["albumId"].ToString();
            string trackId = this.Request.QueryData["trackId"].ToString();

            using (var context = new RunesDbContext())
            {
                Track trackFromDb = context.Tracks.SingleOrDefault(track => track.Id == trackId);

                if (trackFromDb == null)
                {
                    return this.Redirect("/Albums/Details?id={albumId}");
                }

                this.ViewData["Track"] = trackFromDb.ToHtmlDetails();
                this.ViewData["AlbumId"] = albumId;

                return this.View();
            }
        }
    }
}