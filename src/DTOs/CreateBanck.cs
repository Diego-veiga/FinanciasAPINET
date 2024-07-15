using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using financias.src.validations;

namespace financias.src.DTOs
{
    public class CreateBanck
    {
        [CnpjCPFValidation]
        public string Cnpj { get; set; }
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }

    }
}