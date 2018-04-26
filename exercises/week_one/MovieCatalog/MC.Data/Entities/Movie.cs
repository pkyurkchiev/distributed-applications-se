using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.Data.Entities
{
    public class Movie : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string ReleaseCountry { get; set; }

        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
