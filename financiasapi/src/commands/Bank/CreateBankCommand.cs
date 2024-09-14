using System.Text.Json.Serialization;
using MediatR;

namespace financiasapi.src.commands.Bank
{
    public class CreateBankCommand :IRequest
    {
        public string Cnpj { get; set; }
      
        public string Name { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }
        
    }
}