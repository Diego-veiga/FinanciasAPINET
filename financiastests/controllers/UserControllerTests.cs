using AutoFixture;
using financias.src.commands.BankAccount;
using financias.src.controllers;
using financias.src.interfaces;
using financiasapi.src.commands.user;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text.Json;
using Xunit;

namespace financiastests.controllers
{
    public class UserControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<ILogger<UserController>> _loggerMock;
        private  readonly Fixture _fixture;
        private Mock<IUserService> _userServiceMock;

        public UserControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<UserController>>();
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            _userServiceMock = new Mock<IUserService>();
        }

        [Fact]
        public async void Create_ValidCreateUserCommand_SendCommandProcess()
        {
            var userController = new UserController(_userServiceMock.Object,_mediatorMock.Object, _loggerMock.Object);
            var createUserCommand = _fixture.Create<CreateUserCommand>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateBankAccountCommand>(), default))
                      .Returns(Task.CompletedTask);

            var result = await userController.Create(createUserCommand);

            var createdResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(createdResult.StatusCode, 200);
            Assert.Equal(JsonSerializer.Serialize(createdResult.Value), JsonSerializer.Serialize(new { message = "User created successfully" }));
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<CreateUserCommand>(), default), Times.Once);
        }
    }
}
