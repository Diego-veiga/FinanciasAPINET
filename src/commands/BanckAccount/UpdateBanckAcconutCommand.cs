
using System.Text.Json.Serialization;
using MediatR;

namespace financias.src.commands.BanckAccount
{
    public class UpdateBanckAcconutCommand:IRequest
    {
        [JsonIgnore]
       public Guid Id { get; set; }
       public string Name { get; set; }
       public string Type { get; set; }
        public Guid BanckId { get; set; }
       
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
} 