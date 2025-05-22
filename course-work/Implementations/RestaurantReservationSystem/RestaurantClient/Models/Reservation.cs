using System.ComponentModel.DataAnnotations;

namespace RestaurantClient.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int TableId { get; set; }

        [Required]
        [Display(Name = "Дата и час")]
        public DateTime ReservationDate { get; set; }

        [Required]
        [Range(1, 20)]
        [Display(Name = "Брой хора")]
        public int PeopleCount { get; set; }

        [MaxLength(250)]
        [Display(Name = "Бележки")]
        public string? Notes { get; set; }

        public Client? Client { get; set; }
        public Table? Table { get; set; }
    }
}
