using financias.src.commands.BanckAccount;
using financias.src.models.Enums;
using FluentValidation;

namespace financias.src.validations.banckAccount
{
    public class CreateBanckAccountCommandValidation:AbstractValidator<CreateBanckAcconutCommand>
    {
        public CreateBanckAccountCommandValidation()
        {
            RuleFor(c => c.Name)
                               .NotEmpty()
                               .WithMessage("name cannot be empty");
          RuleFor(x => x.Type).Must(i => Enum.IsDefined(typeof(AccountType), i))
                               .WithMessage("Type must have value Current or Savings ");
           RuleFor(x => x.BanckId).NotEmpty()
                                  .WithMessage("BanckId annot be empty");
        }
    }
}
