using System.ComponentModel.DataAnnotations;

namespace MC.Data.Entities
{
    public class Rating : BaseEntity
    {
        [StringLength(10)]
        required public string Name { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
