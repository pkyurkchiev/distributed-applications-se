namespace MC.ApplicationServices.Messaging.Responses
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
