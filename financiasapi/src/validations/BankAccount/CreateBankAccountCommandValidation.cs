using financias.src.commands.BankAccount;
using financiasapi.src.models.Enums;
using FluentValidation;

namespace financiasapi.src.validations.BankAccount
{
    public class CreateBankAccountCommandValidation :AbstractValidator<CreateBankAccountCommand>
    {
        public CreateBankAccountCommandValidation()
        {
            RuleFor(x => x.Name)
                    .NotEmpty()
                    .NotNull()
                    .MaximumLength(30)
                    .WithMessage("The name of the bank account must have a maximum of 30 characters");
            RuleFor(x => x.BankId)
                   .NotEmpty()
                   .NotNull()
                   .WithMessage("BankId is required");
            RuleFor(x => x.Type)
                   .IsEnumName(typeof(AccountType))
                   .WithMessage($" Type of BankAccount must be {string.Join(", ", Enum.GetNames(typeof(AccountType)))}");
                  
            


        }
        
    }
}