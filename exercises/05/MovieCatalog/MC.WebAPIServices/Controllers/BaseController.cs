using System.Web.Http;

namespace MC.WebAPIServices.Controllers
{
    public class BaseController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Version()
        {
            return Json("Web API version 1.0");
        }
    }
}
