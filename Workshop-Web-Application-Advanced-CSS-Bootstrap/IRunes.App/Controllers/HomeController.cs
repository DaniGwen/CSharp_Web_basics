using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.App.Controllers
{
    public class HomeController : BaseController
    {
        public IHttpResponse Index(IHttpRequest httpRequest)
        {
            if (this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("Home");
            }

            return this.View();
        }
    }
}
