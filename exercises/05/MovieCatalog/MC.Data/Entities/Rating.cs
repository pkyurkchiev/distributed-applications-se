using System.ComponentModel.DataAnnotations;

namespace MC.Data.Entities
{
    public class Rating : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
