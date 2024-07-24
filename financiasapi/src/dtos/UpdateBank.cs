
using System.ComponentModel.DataAnnotations;
using financiasapi.src.validations;

namespace financiasapi.src.dtos
{
    public class UpdateBank
    {
       [Obsolete]
        public Guid Id { get; set; }
        [CnpjCPFValidation]
        public string Cnpj { get; set; }
        [Required]
        public string Name { get; set; } 
    }
}