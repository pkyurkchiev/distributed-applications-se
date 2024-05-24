namespace MC.Infrastructure.Messaging.Responses.Movies
{
    /// <summary>
    /// Get movies repsonse object.
    /// </summary>
    public class GetMoviesResponse : ServiceResponseBase
    {
        /// <summary>
        /// Gets or sets the movies list.
        /// </summary>
        public List<MovieViewModel>? Movies { get; set; }
    }
}
