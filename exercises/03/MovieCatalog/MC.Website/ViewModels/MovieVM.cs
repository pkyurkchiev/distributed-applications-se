using System;
using System.ComponentModel.DataAnnotations;

namespace MC.Website.ViewModels
{
    public class MovieVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string ReleaseCountry { get; set; }

        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        public GenreVM GenreVM { get; set; }
    }
}