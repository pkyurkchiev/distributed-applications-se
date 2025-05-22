using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RestaurantClient.Models
{
    public class ReservationFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Клиент")]
        public int ClientId { get; set; }

        [Required]
        [Display(Name = "Маса")]
        public int TableId { get; set; }

        [Required]
        [Display(Name = "Дата и час")]
        public DateTime ReservationDate { get; set; }

        [Required]
        [Display(Name = "Брой хора")]
        public int PeopleCount { get; set; }

        [Display(Name = "Бележки")]
        public string? Notes { get; set; }

        public List<SelectListItem> Clients { get; set; } = new();
        public List<SelectListItem> Tables { get; set; } = new();
    }
}
