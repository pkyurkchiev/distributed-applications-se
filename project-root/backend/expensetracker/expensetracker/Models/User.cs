using System.ComponentModel.DataAnnotations;
using System.Security.Principal;



namespace expensetracker.Models
{
    public class User
    {

        
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        // Навигация
        public List<Account> Accounts { get; set; }


    }
}
