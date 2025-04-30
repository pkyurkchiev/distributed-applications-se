using System.ComponentModel.DataAnnotations;

namespace MC.Data.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        required public int CreatedBy { get; set; }
        required public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set;}
        public DateTime? UpdatedOn { get; set;}

        required public bool IsActivated { get; set; }
    }
}
