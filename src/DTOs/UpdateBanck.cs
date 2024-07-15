using System.ComponentModel.DataAnnotations;
using financias.src.validations;

namespace financias.src.DTOs
{
    public class UpdateBanck
    {
        [Obsolete]
        public Guid Id { get; set; }
        [CnpjCPFValidation]
        public string Cnpj { get; set; }
        [Required]
        public string Name { get; set; }
    }
}