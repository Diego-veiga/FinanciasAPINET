using MediatR;
using System.Text.Json.Serialization;


namespace financiasapi.src.commands.user
{
    public class UpdateUserCommand : IRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
