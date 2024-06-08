
using System.ComponentModel.DataAnnotations;
using financias.src.utils;

namespace financias.src.validation
{
    public class CnpjCPFValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult("CNPJ/CPF invalid");
            }
            var teste = CpfCnpjValidate.IsValid((string)value);
            return CpfCnpjValidate.IsValid((string)value)
                                                          ? ValidationResult.Success
                                                         : new ValidationResult("CNPJ/CPF invalid");


        }
    }
}