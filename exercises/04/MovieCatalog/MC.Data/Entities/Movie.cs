namespace MC.Data.Entities
{
    public class Movie : BaseEntity
    {
        required public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Country { get; set; }
        public string? Studio { get; set; }

        public int? RatingId { get; set; }
        public virtual Rating Rating { get; set; }

        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
