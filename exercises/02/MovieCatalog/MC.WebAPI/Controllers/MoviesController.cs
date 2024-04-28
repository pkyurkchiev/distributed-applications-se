using MC.ApplicationServices.Interfaces;
using MC.ApplicationServices.Messaging.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MC.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieManagementService _movieManagement;

        public MoviesController(IMovieManagementService movieManagemen)
        {
            _movieManagement = movieManagemen;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            return Ok(await _movieManagement.GetMovies());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MovieModel movie)
        {
            return Ok(await _movieManagement.CreateMovie(new(movie)));
        }


        //[HttpGet("search/{title}")]
        //public async Task<IActionResult> Get([FromRoute] string title)
        //{
        //    return Ok(await Context.Movies.Where(x => x.Title.Equals(title)).ToListAsync());
        //}
    }
}
