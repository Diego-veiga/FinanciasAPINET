using financiasapi.src.commands.user;
using FluentValidation;

namespace financiasapi.src.validations.User
{
    public class CreateUserCommandValidation: AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidation()
        {
            RuleFor(x => x.Name)
                  .NotEmpty()
                  .MaximumLength(30)
                  .WithMessage("The name of the bank must have a maximum of 30 characters");

            RuleFor(x => x.Password)
                    .MinimumLength(4)
                    .WithMessage("The user's password must have a minimum of 4 characters and a maximum of 30 characters.")
                    .MaximumLength(30)
                    .WithMessage("The user's password must have a minimum of 4 characters and a maximum of 30 characters.");

            RuleFor(x => x.Email)
                   .NotEmpty()
                   .EmailAddress()
                   .WithMessage("Please enter a valid email address.");
        }
    }
}
