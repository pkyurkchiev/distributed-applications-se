using System;

namespace MC.ApplicationServices.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string ReleaseCountry { get; set; }
        public GenreDto Genre { get; set; }
    }
}
