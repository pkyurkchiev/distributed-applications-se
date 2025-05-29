using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(15)]
        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        public DateTime RegisteredOn { get; set; } = DateTime.Now;

        public bool IsVip { get; set; } = false;

    }
}
