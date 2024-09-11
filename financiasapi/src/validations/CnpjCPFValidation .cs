using System.ComponentModel.DataAnnotations;
using financiasapi.src.utils;

namespace financiasapi.src.validations {
       public class CnpjCPFValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult("CNPJ/CPF invalid");
            }
            
            return CpfCnpjValidate.IsValid((string)value)
                                                          ? ValidationResult.Success
                                                         : new ValidationResult("CNPJ/CPF invalid");


        }
    }
}
