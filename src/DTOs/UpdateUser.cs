using System.ComponentModel.DataAnnotations;

namespace financias.src.DTOs
{
    public class UpdateUser
    {
        [Obsolete]
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [StringLength(50, MinimumLength = 4)]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}