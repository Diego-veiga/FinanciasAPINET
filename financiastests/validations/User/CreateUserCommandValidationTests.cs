using AutoFixture;
using financiasapi.src.commands.user;
using financiasapi.src.validations.User;
using Xunit;

namespace financiastests.validations.User
{
    public class CreateUserCommandValidationTests
    {
        public readonly Fixture _fixture;
        public CreateUserCommandValidationTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
        }

        [Fact]
        public void CreateUserCommandValidation_ObjectValid_ReturnValidTrue()
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
    }
}
