using System.ComponentModel.DataAnnotations;

namespace MC.Data.Entities
{
    public class Rating : BaseEntity
    {
        [StringLength(10)]
        required public string Name { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual ICollection<Movie> Movies { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
