using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace expensetracker.Models
{
    public class Transaction
    {
        
        public int Id { get; set; }

        [Required]
        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public Account? Account { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } 

        [Required]
        [Range(0.01, double.MaxValue)]
        public double Amount { get; set; }

        [Required]
        [MaxLength(100)]
        public string Category { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(255)]
        public string? Note { get; set; }
    }
}
