using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int TableId { get; set; }

        public DateTime ReservationDate { get; set; }

        public int PeopleCount { get; set; }

        [MaxLength(250)]
        public string? Notes { get; set; }

        public Client? Client { get; set; }
        public Table? Table { get; set; }
    }
        
    }
