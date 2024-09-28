using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using MediatR;

namespace financiasapi.src.commands.Bank
{
    [ExcludeFromCodeCoverage]
    public class DeleteBankCommand:IRequest
    {
        [JsonIgnore]
       public Guid Id { get; set; }
    }
}