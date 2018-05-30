using System.ComponentModel.DataAnnotations;

namespace MC.Website.ViewModels
{
    public class GenreVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}