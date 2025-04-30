namespace MC.Infrastructure.Messaging.Responses.Movies
{
    public class MovieViewModel
    {
        required public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Country { get; set; }
        public string? Studio { get; set; }
        required public string Genre { get; set; }
        public string? Rating { get; set; }
    }
}
