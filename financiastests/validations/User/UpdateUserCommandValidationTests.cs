using AutoFixture;
using financias.src.commands.BankAccount;
using financiasapi.src.commands.user;
using financiasapi.src.validations.BankAccount;
using financiasapi.src.validations.User;
using Xunit;

namespace financiastests.validations.User
{
    public class UpdateUserCommandValidationTests
    {
        public readonly Fixture _fixture;
        public UpdateUserCommandValidationTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
        }

        [Fact]
        public void UpdateUserCommandValidation_ObjectValid_ReturnValidTrue()
        {
            var createUserCommand = _fixture.Create<CreateUserCommand>();
            createUserCommand.Name = "Teste 123";
            createUserCommand.Email = "teste123@hotmail.com";
            createUserCommand.Password = "1234";
            var createUserCommandValidation = new CreateUserCommandValidation();

            var result = createUserCommandValidation.Validate(createUserCommand);

            Assert.True(result.IsValid);
            Assert.Equal(result.Errors.Count, 0);
        }

        [Fact]
        public void UpdateUserCommandValidation_ObjectWithNameFieldGreaterThanThirty_ReturnValidFalse()
        {
            var createUserCommand = _fixture.Create<CreateUserCommand>();
            createUserCommand.Name = "Teste 123456789asdfghjklçpoiuytrewqazxcvbnmnqwertyuiopçlkj";
            createUserCommand.Email = "teste123@hotmail.com";
            createUserCommand.Password = "1234";
            var createUserCommandValidation = new CreateUserCommandValidation();

            var result = createUserCommandValidation.Validate(createUserCommand);

            Assert.False(result.IsValid);
            Assert.Equal(result.Errors.Count, 1);
            Assert.Equal(result.Errors[0].ErrorMessage, "The name of the bank must have a maximum of 30 characters");
        }

        [Fact]
        public void UpdateUserCommandValidation_ObjectWithNameFieldNull_ReturnValidFalse()
        {
            var createUserCommand = _fixture.Create<CreateUserCommand>();
            createUserCommand.Name = null;
            createUserCommand.Email = "teste123@hotmail.com";
            createUserCommand.Password = "1234";
            var createUserCommandValidation = new CreateUserCommandValidation();

            var result = createUserCommandValidation.Validate(createUserCommand);

            Assert.False(result.IsValid);
            Assert.Equal(result.Errors.Count, 1);
            Assert.Equal(result.Errors[0].ErrorMessage, "'Name' must not be empty.");
        }

        [Fact]
        public void UpdateUserCommandValidation_ObjectWithNameFielEmpty_ReturnValidFalse()
        {
            var createUserCommand = _fixture.Create<CreateUserCommand>();
            createUserCommand.Name = "  ";
            createUserCommand.Email = "teste123@hotmail.com";
            createUserCommand.Password = "1234";
            var createUserCommandValidation = new CreateUserCommandValidation();

            var result = createUserCommandValidation.Validate(createUserCommand);

            Assert.False(result.IsValid);
            Assert.Equal(result.Errors.Count, 1);
            Assert.Equal(result.Errors[0].ErrorMessage, "'Name' must not be empty.");
        }

        [Fact]
        public void UpdateUserCommandValidation_ObjectWithPassawordFieldLessThanFour_ReturnValidFalse()
        {
            var createUserCommand = _fixture.Create<CreateUserCommand>();
            createUserCommand.Name = "Teste";
            createUserCommand.Email = "teste123@hotmail.com";
            createUserCommand.Password = "12";
            var createUserCommandValidation = new CreateUserCommandValidation();

            var result = createUserCommandValidation.Validate(createUserCommand);

            Assert.False(result.IsValid);
            Assert.Equal(result.Errors.Count, 1);
            Assert.Equal(result.Errors[0].ErrorMessage, "The user's password must have a minimum of 4 characters and a maximum of 30 characters.");
        }

        [Fact]
        public void UpdateUserCommandValidation_ObjectWithPassawordFieldGreaterThanThirty_ReturnValidFalse()
        {
            var createUserCommand = _fixture.Create<CreateUserCommand>();
            createUserCommand.Name = "Teste";
            createUserCommand.Email = "teste123@hotmail.com";
            createUserCommand.Password = "0123456789012345678901234567890123456789";
            var createUserCommandValidation = new CreateUserCommandValidation();

            var result = createUserCommandValidation.Validate(createUserCommand);

            Assert.False(result.IsValid);
            Assert.Equal(result.Errors.Count, 1);
            Assert.Equal(result.Errors[0].ErrorMessage, "The user's password must have a minimum of 4 characters and a maximum of 30 characters.");
        }

        [Fact]
        public void UpdateUserCommandValidation_ObjectWithEmailFieldInvalid_ReturnValidFalse()
        {
            var createUserCommand = _fixture.Create<CreateUserCommand>();
            createUserCommand.Name = "Teste";
            createUserCommand.Email = "teste";
            createUserCommand.Password = "01234";
            var createUserCommandValidation = new CreateUserCommandValidation();

            var result = createUserCommandValidation.Validate(createUserCommand);

            Assert.False(result.IsValid);
            Assert.Equal(result.Errors.Count, 1);
            Assert.Equal(result.Errors[0].ErrorMessage, "Please enter a valid email address.");
        }

        [Fact]
        public void UpdateUserCommandValidation_ObjectWithEmailFieldNull_ReturnValidFalse()
        {
            var createUserCommand = _fixture.Create<CreateUserCommand>();
            createUserCommand.Name = "Teste";
            createUserCommand.Email = null;
            createUserCommand.Password = "01234";
            var createUserCommandValidation = new CreateUserCommandValidation();

            var result = createUserCommandValidation.Validate(createUserCommand);

            Assert.False(result.IsValid);
            Assert.Equal(result.Errors.Count, 1);
            Assert.Equal(result.Errors[0].ErrorMessage, "'Email' must not be empty.");
        }
    }
}
