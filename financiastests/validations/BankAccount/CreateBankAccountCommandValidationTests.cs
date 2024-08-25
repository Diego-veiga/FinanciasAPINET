using AutoFixture;
using financias.src.commands.BankAccount;
using financiasapi.src.validations.BankAccount;
using Xunit;

namespace financiastests.validations.BankAccount
{
    public class CreateBankAccountCommandValidationTests
    {
           public readonly Fixture _fixture ;
           public CreateBankAccountCommandValidationTests()
           {
                _fixture = new Fixture();
                _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _fixture.Behaviors.Remove(b));
                _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
           }
           
        [Fact]
        public void CreateBankAccountCommandValidation_ObjectValid_ReturnValidTrue()
        {
            var createBankAccountCommand = _fixture.Create<CreateBankAccountCommand>();
             createBankAccountCommand.Name = "tests";
            createBankAccountCommand.Type = "Savings";
            var createBankAccountCommandValidation  = new CreateBankAccountCommandValidation();

            var result = createBankAccountCommandValidation.Validate(createBankAccountCommand);

           Assert.True(result.IsValid);
           Assert.Equal(result.Errors.Count, 0);
        }

        [Fact]
        public void CreateBankAccountCommandValidation_ObjectWithTypeFieldInvalid_ReturnValidFalse()
        {
            var createBankAccountCommand = _fixture.Create<CreateBankAccountCommand>();
             createBankAccountCommand.Name = "tests";
            var createBankAccountCommandValidation  = new CreateBankAccountCommandValidation();

            var result = createBankAccountCommandValidation.Validate(createBankAccountCommand);

           Assert.False(result.IsValid);
           Assert.Equal(result.Errors.Count, 1);
           Assert.Equal(result.Errors[0].ErrorMessage, "Type of BankAccount must be Current, Savings");
        }

        [Fact]
        public void CreateBankAccountCommandValidation_ObjectWithNameFieldGreaterThan30_ReturnValidFalse()
        {
            var createBankAccountCommand = _fixture.Create<CreateBankAccountCommand>();
            var createBankAccountCommandValidation  = new CreateBankAccountCommandValidation();
            createBankAccountCommand.Type = "Savings";

            var result = createBankAccountCommandValidation.Validate(createBankAccountCommand);

           Assert.False(result.IsValid);
           Assert.Equal(result.Errors.Count, 1);
           Assert.Equal(result.Errors[0].ErrorMessage, "The name of the bank account must have a maximum of 30 characters");
        }

        [Fact]
        public void CreateBankAccountCommandValidation_ObjectWithBankFieldEmpty_ReturnValidFalse()
        {
            var createBankAccountCommand = _fixture.Create<CreateBankAccountCommand>();
            var createBankAccountCommandValidation  = new CreateBankAccountCommandValidation();
            createBankAccountCommand.Name = "tests";
            createBankAccountCommand.Type = "Savings";
            createBankAccountCommand.BankId = Guid.Empty;

            var result = createBankAccountCommandValidation.Validate(createBankAccountCommand);

           Assert.False(result.IsValid);
           Assert.Equal(result.Errors.Count, 1);
           Assert.Equal(result.Errors[0].ErrorMessage, "BankId invalid");
        }

        [Fact]
        public void CreateBankAccountCommandValidation_ObjectWithBankFieldNull_ReturnValidFalse()
        {
            var createBankAccountCommand = new CreateBankAccountCommand()
            {
                Name = "tests",
                Type = "Savings",
                UserId = Guid.NewGuid()
            };
            var createBankAccountCommandValidation  = new CreateBankAccountCommandValidation();
        
            var result = createBankAccountCommandValidation.Validate(createBankAccountCommand);

           Assert.False(result.IsValid);
           Assert.Equal(result.Errors.Count, 1);
           Assert.Equal(result.Errors[0].ErrorMessage, "BankId invalid");
        }
    }
}