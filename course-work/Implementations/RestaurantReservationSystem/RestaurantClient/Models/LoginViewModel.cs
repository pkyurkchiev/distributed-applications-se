using System.ComponentModel.DataAnnotations;

namespace RestaurantClient.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; } = string.Empty;
    }
}
