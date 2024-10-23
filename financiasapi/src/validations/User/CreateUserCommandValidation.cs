using financiasapi.src.commands.user;
using financiasapi.src.utils;
using FluentValidation;

namespace financiasapi.src.validations.User
{
    public class CreateUserCommandValidation: AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidation()
        {
            RuleFor(x => x.Name)
                  .NotEmpty()
                  .NotNull()
                  .MaximumLength(30)
                  .WithMessage("The name of the bank must have a maximum of 30 characters");

            RuleFor(x => x.Password)
                    .NotEmpty()
                    .NotNull()
                    .MinimumLength(4)
                    .MaximumLength(30)
                    .WithMessage("The user's password must have a minimum of 4 characters and a maximum of 30 characters.");
            
            RuleFor(x => x.Email)
                   .NotEmpty()
                   .NotNull()
                   .EmailAddress()
                   .WithMessage("Please enter a valid email address.");
        }
    }
}
