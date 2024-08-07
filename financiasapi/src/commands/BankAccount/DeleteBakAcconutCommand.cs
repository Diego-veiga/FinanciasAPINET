
using System.Text.Json.Serialization;
using MediatR;

namespace financias.src.commands.BankAccount
{
    public class DeleteBankAccountCommand:IRequest
    {
        [JsonIgnore]
       public Guid Id { get; set; }
      
    }
} 