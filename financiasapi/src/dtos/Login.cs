using System.ComponentModel.DataAnnotations;

namespace financiasapi.src.dtos
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