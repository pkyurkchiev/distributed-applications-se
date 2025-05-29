using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi.Models
{
   public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Username { get; set; }

    [Required]
    public string? PasswordHash { get; set; }

    [Required]
    [MaxLength(100)]
    public string? FullName { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Range(1, 120)]
    public int Age { get; set; }
}

}