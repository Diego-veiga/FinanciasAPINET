using MediatR;


namespace financiasapi.src.commands.user
{
    public class CreateUserCommand : IRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
