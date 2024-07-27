
using System.Text.Json.Serialization;
using MediatR;

namespace financias.src.commands.BankAccount
{
    public class CreateBankAccountCommand:IRequest
    {
       public string Name { get; set; }
       public string Type { get; set; }
        public Guid BanckId { get; set; }
       
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
} 