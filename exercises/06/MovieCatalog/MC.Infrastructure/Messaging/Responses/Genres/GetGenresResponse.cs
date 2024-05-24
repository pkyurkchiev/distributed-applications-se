namespace MC.Infrastructure.Messaging.Responses.Genres
{
    /// <summary>
    /// Get genres repsonse object.
    /// </summary>
    public class GetGenresResponse : ServiceResponseBase
    {
        /// <summary>
        /// Gets or sets the genres list.
        /// </summary>
        public List<GenreViewModel>? Genres { get; set; }
    }
}
