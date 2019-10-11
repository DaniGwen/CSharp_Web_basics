namespace IRunes.App.Controllers
{
    using SIS.HTTP.Requests;
    using SIS.HTTP.Responses;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attributes.Action;

    public class InfoController : Controller
    {
        public IHttpResponse About(IHttpRequest httpRequest)
        {
            return this.View();
        }

        [NonAction]
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
