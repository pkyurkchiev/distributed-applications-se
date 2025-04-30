namespace MC.Data.Entities
{
    public class Movie : BaseEntity
    {
        required public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Country { get; set; }
        public string? Studio { get; set; }

        public int? RatingId { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual Rating Rating { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public int GenreId { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual Genre Genre { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
