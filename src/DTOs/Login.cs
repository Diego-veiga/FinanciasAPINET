
using System.ComponentModel.DataAnnotations;

namespace financias.src.DTOs
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Password { get; set; }
    }
}