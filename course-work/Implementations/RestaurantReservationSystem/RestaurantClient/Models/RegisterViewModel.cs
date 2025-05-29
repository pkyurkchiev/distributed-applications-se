using System.ComponentModel.DataAnnotations;

namespace RestaurantClient.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат")]
        [Display(Name = "Потвърди паролата")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Име")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Range(1, 120)]
        [Display(Name = "Години")]
        public int Age { get; set; }
    }
}
