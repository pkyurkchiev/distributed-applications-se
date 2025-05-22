using System.ComponentModel.DataAnnotations;

namespace RestaurantClient.Models
{
    public class Table
    {
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        [MaxLength(100)]
        public string Location { get; set; } = string.Empty;
    }
}
