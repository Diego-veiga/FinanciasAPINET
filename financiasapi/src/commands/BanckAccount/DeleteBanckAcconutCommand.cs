
using System.Text.Json.Serialization;
using MediatR;

namespace financias.src.commands.BanckAccount
{
    public class DeleteBanckAcconutCommand:IRequest
    {
        [JsonIgnore]
       public Guid Id { get; set; }
      
    }
} 