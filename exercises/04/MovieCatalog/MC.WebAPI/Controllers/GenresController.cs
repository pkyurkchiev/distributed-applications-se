using MC.ApplicationServices.Interfaces;
using MC.ApplicationServices.Messaging;
using MC.ApplicationServices.Messaging.Requests;
using MC.ApplicationServices.Messaging.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MC.WebAPI.Controllers
{
    /// <summary>
    /// Genre controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class GenresController : ControllerBase
    {
        private readonly IGenresManagementService _genreService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenresController"/> class.
        /// </summary>
        /// <param name="genreService">Movie management service.</param>
        public GenresController(IGenresManagementService genreService)
        {
            _genreService = genreService;
        }

        /// <summary>
        /// Get genres.
        /// </summary>
        /// <returns>Return list of active genre.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetGenresResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get() 
        {
            return Ok(await _genreService.GetGenres());
        }

        /// <summary>
        /// Create new genre.
        /// </summary>
        /// <param name="genre">Genre model object.</param>
        /// <returns>Return create empty response.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateGenreResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] GenreModel genre)
        {
            return Ok(await _genreService.CreateGenre(new(genre)));
        }

        /// <summary>
        /// Delete genre.
        /// </summary>
        /// <param name="id">Genre identifier.</param>
        /// <returns>Return empty object.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteGenreResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteGenre([FromRoute] int id) => Ok(await _genreService.DeleteGenre(new(id)));
    }
}
