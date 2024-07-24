using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using financiasapi.src.validations;

namespace financiasapi.src.dtos
{
    public class CreateBank
    {
         [CnpjCPFValidation]
        public string Cnpj { get; set; }
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}