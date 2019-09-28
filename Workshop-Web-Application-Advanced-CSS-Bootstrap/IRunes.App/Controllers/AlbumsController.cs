using IRunes.Data;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses;
using SIS.HTTP.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunes.App.Controllers
{
    public class AlbumsController : BaseController
    {
        public IHttpResponse All(IHttpRequest httpRequest)
        {
            using(var context = new RunesDbContext())
            {
                this.ViewData["Albums"] = context.Albums.Select(album => album).ToList();

                return this.View();
            }
        }

        public IHttpResponse Create(IHttpRequest httpRequest)
        {
            return null;
        } 

        public IHttpResponse Details(IHttpRequest httpRequest)
        {
            return null;
        }
    }
}
