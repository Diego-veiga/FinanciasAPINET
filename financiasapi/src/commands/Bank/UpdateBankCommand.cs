using System.Text.Json.Serialization;
using MediatR;


namespace financiasapi.src.commands.Bank
{
    public class UpdateBankCommand:IRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        
        public string Cnpj { get; set; }
        
        public string Name { get; set; }
        
    }
}