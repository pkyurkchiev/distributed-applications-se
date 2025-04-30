using MC.Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MC.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private MovieCatalogDbContext Context { get; set; }

        public MoviesController(MovieCatalogDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            return Ok(await Context.Movies.ToListAsync());
        }

        [HttpGet("search/{title}")]
        public async Task<IActionResult> Get([FromRoute] string title)
        {
            return Ok(await Context.Movies.Where(x => x.Title.Equals(title)).ToListAsync());
        }
    }
}
