using MC.ApplicationServices.Interfaces;
using MC.ApplicationServices.Messaging;
using MC.ApplicationServices.Messaging.Requests;
using MC.ApplicationServices.Messaging.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MC.WebAPI.Controllers
{
    /// <summary>
    /// Movies controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesManagementService _movieService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoviesController"/> class.
        /// </summary>
        /// <param name="movieService">Movie management service.</param>
        public MoviesController(IMoviesManagementService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Get movies.
        /// </summary>
        /// <param name="isActive">Is active boolean type.</param>
        /// <returns>Return list of active movies.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetMoviesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] bool isActive = true) => Ok(await _movieService.GetMovies(new(isActive)));

        /// <summary>
        /// Create new movie.
        /// </summary>
        /// <param name="movie">Movie model object.</param>
        /// <returns>Return create empty response.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateMovieResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMovie([FromBody] MovieModel movie) => Ok(await _movieService.CreateMovie(new(movie)));

        /// <summary>
        /// Delete movie.
        /// </summary>
        /// <param name="id">Movie identifier.</param>
        /// <returns>Return empty object.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteMovieResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMovie([FromRoute] int id) => Ok(await _movieService.DeleteMovie(new(id)));
    }
}
