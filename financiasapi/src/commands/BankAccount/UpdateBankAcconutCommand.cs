
using System.Text.Json.Serialization;
using MediatR;

namespace financias.src.commands.BankAccount
{
    public class UpdateBankAccountCommand:IRequest
    {
        [JsonIgnore]
       public Guid Id { get; set; }
       public string Name { get; set; }
       public string Type { get; set; }
        public Guid BankId { get; set; }
       
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
} 