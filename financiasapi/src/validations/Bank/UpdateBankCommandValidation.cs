using financiasapi.src.commands.Bank;
using financiasapi.src.utils;
using FluentValidation;

namespace financiasapi.src.validations.Bank
{
    public class UpdateBankCommandValidation : AbstractValidator<UpdateBankCommand>
    {
        public UpdateBankCommandValidation()
        {
            RuleFor(x => x.Name)
                   .NotEmpty()
                   .NotNull()
                   .MaximumLength(30)
                   .WithMessage("The name of the bank must have a maximum of 30 characters");
            
            RuleFor(x => x.Cnpj).Custom((value, context)=>{
                 if(!CpfCnpjValidate.IsValid(value)){
                       context.AddFailure("Document invalid");
                 }
                   
            });      
        }
    }
}