using System.Text.Json.Serialization;
using MediatR;

namespace financiasapi.src.commands.Bank
{
    public class DeleteBankCommand:IRequest
    {
        [JsonIgnore]
       public Guid Id { get; set; }
    }
}