using MC.ApplicationServices.Interfaces;
using MC.Infrastructure.Messaging.Responses.Authentications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MC.WebAPI.Controllers
{
    /// <summary>
    /// Authentication controller.
    /// </summary>
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="jwtAuthenticationManager">Jwt authentication manager</param>
        public AuthController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        /// <summary>
        /// Generate Jwt token.
        /// </summary>
        /// <param name="clientId">Client identifier.</param>
        /// <param name="secret">Client secret</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Token([FromQuery] string clientId, [FromQuery] string secret)
        {
            string? token = _jwtAuthenticationManager.Authenticate(clientId, secret);

            ArgumentNullException.ThrowIfNull(token);


            return Ok(await Task.FromResult(new AuthenticationResponse(token)));
        }
    }
}
