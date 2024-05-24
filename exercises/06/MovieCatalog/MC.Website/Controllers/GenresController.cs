using MC.WebWCF;
using Microsoft.AspNetCore.Mvc;

namespace MC.Website.Controllers
{
    public class GenresController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (GenresServiceClient client = new())
            {
                return View(await client.GetGenresAsync());
            }
        }
    }
}
