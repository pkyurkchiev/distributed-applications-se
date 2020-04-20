using System.ComponentModel.DataAnnotations;

namespace MC.Data.Entities
{
    public class Director : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
