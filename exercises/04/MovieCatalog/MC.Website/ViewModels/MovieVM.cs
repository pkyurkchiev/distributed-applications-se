using MC.Website.MoviesReference;
using System;
using System.ComponentModel.DataAnnotations;

namespace MC.Website.ViewModels
{
    public class MovieVM
    {
        #region Properties
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Display(Name = "Release date")]
        public DateTime? ReleaseDate { get; set; }
        [Display(Name = "Release country")]
        public string ReleaseCountry { get; set; }

        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        public GenreVM GenreVM { get; set; }
        #endregion

        #region Contructors
        public MovieVM() { }

        public MovieVM(MovieDto movieDto)
        {
            Id = movieDto.Id;
            Title = movieDto.Title;
            ReleaseDate = movieDto.ReleaseDate;
            ReleaseCountry = movieDto.ReleaseCountry;
            GenreId = movieDto.Genre.Id;
            GenreVM = new GenreVM
            {
                Id = movieDto.Genre.Id,
                Name = movieDto.Genre.Name
            };
        }
        #endregion
    }
}