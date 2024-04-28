namespace MC.ApplicationServices.Messaging.Requests
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
