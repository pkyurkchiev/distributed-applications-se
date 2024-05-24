namespace MC.Infrastructure.Messaging.Requests.Movies
{
    public class MovieModel
    {
        required public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Country { get; set; }
        public string? Studio { get; set; }
        required public int GenreId { get; set; }
        public int? RatingId { get; set; }
    }
}
