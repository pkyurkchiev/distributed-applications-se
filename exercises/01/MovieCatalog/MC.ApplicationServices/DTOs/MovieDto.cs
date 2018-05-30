using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.ApplicationServices.DTOs
{
    public class MovieDto
    {
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string ReleaseCountry { get; set; }
        public GenreDto Genre { get; set; }
    }
}
