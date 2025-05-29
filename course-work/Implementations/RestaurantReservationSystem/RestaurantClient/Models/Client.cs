using System.ComponentModel.DataAnnotations;

namespace RestaurantClient.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public DateTime RegisteredOn { get; set; } = DateTime.Now;

        public bool IsVip { get; set; }
    }
}
