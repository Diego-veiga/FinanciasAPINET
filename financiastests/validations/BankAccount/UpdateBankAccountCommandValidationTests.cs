using AutoFixture;
using financias.src.commands.BankAccount;
using financiasapi.src.validations.BankAccount;
using Xunit;

namespace financiastests.validations.BankAccount
{
    public class UpdateBankAccountCommandValidationTests
    {
        public readonly Fixture _fixture ;
           public UpdateBankAccountCommandValidationTests()
           {
                _fixture = new Fixture();
                _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _fixture.Behaviors.Remove(b));
                _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
           }
         [Fact]
        public void UpdateBankAccountCommandValidation_ObjectValid_ReturnValidTrue()
        {
            var updateBankAccountCommand = _fixture.Create<UpdateBankAccountCommand>();
             updateBankAccountCommand.Name = "tests";
            updateBankAccountCommand.Type = "Savings";
            var updateBankAccountCommandValidation  = new UpdateBankAccountCommandValidation();

            var result = updateBankAccountCommandValidation.Validate(updateBankAccountCommand);

           Assert.True(result.IsValid);
           Assert.Equal(result.Errors.Count, 0);
        }

        [Fact]
        public void UpdateBankAccountCommandValidation_ObjectWithTypeFieldInvalid_ReturnValidFalse()
        {
            var updateBankAccountCommand = _fixture.Create<UpdateBankAccountCommand>();
             updateBankAccountCommand.Name = "tests";
            var updateBankAccountCommandValidation  = new UpdateBankAccountCommandValidation();

            var result = updateBankAccountCommandValidation.Validate(updateBankAccountCommand);

           Assert.False(result.IsValid);
           Assert.Equal(result.Errors.Count, 1);
           Assert.Equal(result.Errors[0].ErrorMessage, "Type of BankAccount must be Current, Savings");
        }

        [Fact]
        public void UpdateBankAccountCommandValidation_ObjectWithNameFieldGreaterThan30_ReturnValidFalse()
        {
            var updateBankAccountCommand = _fixture.Create<UpdateBankAccountCommand>();
            var updateBankAccountCommandValidation  = new UpdateBankAccountCommandValidation();
            updateBankAccountCommand.Type = "Savings";

            var result = updateBankAccountCommandValidation.Validate(updateBankAccountCommand);

           Assert.False(result.IsValid);
           Assert.Equal(result.Errors.Count, 1);
           Assert.Equal(result.Errors[0].ErrorMessage, "The name of the bank account must have a maximum of 30 characters");
        }

        [Fact]
        public void UpdateBankAccountCommandValidation_ObjectWithBankFieldEmpty_ReturnValidFalse()
        {
            var updateBankAccountCommand = _fixture.Create<UpdateBankAccountCommand>();
            var updateBankAccountCommandValidation  = new UpdateBankAccountCommandValidation();
            updateBankAccountCommand.Name = "tests";
            updateBankAccountCommand.Type = "Savings";
            updateBankAccountCommand.BankId = Guid.Empty;

            var result = updateBankAccountCommandValidation.Validate(updateBankAccountCommand);

           Assert.False(result.IsValid);
           Assert.Equal(result.Errors.Count, 1);
           Assert.Equal(result.Errors[0].ErrorMessage, "BankId invalid");
        }

        [Fact]
        public void UpdateBankAccountCommandValidation_ObjectWithBankFieldNull_ReturnValidFalse()
        {
            var updateBankAccountCommand = new UpdateBankAccountCommand()
            {
                Name = "tests",
                Type = "Savings",
                UserId = Guid.NewGuid()
            };
            var updateBankAccountCommandValidation  = new UpdateBankAccountCommandValidation();
        
            var result = updateBankAccountCommandValidation.Validate(updateBankAccountCommand);

           Assert.False(result.IsValid);
           Assert.Equal(result.Errors.Count, 1);
           Assert.Equal(result.Errors[0].ErrorMessage, "BankId invalid");
        }
    }
}