using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace expensetracker.Models
{
    public class Account
    {
       
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }  // "cash", "card", "combined"

        [Required]
        [Range(0, double.MaxValue)]
        public double Balance { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Навигация
        public List<Transaction>? Transactions { get; set; }
    }
}
