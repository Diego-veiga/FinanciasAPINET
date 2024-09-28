using System.Security.Claims;
using AutoFixture;
using financias.src.controllers;
using financias.src.interfaces;
using financiasapi.src.commands.Bank;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace financiastests.controllers
{
    public class BankControllerTests
    {
        private  Mock<IMediator> _mediatorMock;
        private Mock<ILogger<BankController>> _loggerMock;
         private Mock<IBankService> _bankServiceMock;
        public readonly Fixture _fixture ;

        public BankControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<BankController>>();
            _bankServiceMock = new Mock<IBankService>();
             _fixture = new Fixture();
                _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _fixture.Behaviors.Remove(b));
                    _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
        }
        
        [Fact]
        public async void Create_ValidCreateCommand_SendCommandProcess()
        {
            var bankController = new BankController(_mediatorMock.Object,_bankServiceMock.Object,_loggerMock.Object);
            var claims = new List<Claim>
            {
                new Claim("id", Guid.NewGuid().ToString())
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            bankController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

            var createBankCommand = _fixture.Create<CreateBankCommand>();
           _mediatorMock.Setup(m => m.Send(It.IsAny<CreateBankCommand>(),default))
                     .Returns(Task.CompletedTask);

            var result = await bankController.Create(createBankCommand);

             var createdResult = Assert.IsType<CreatedAtRouteResult>(result);
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<CreateBankCommand>(), default), Times.Once);
        }

         [Fact]
        public async void Update_ValidUpdateBankCommand_SendCommandProcess()
        {
            var bankController = new BankController(_mediatorMock.Object,_bankServiceMock.Object,_loggerMock.Object);
            var claims = new List<Claim>
            {
                new Claim("id", Guid.NewGuid().ToString())
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            bankController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

             var updateBankCommand = _fixture.Create<UpdateBankCommand>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateBankCommand>(),default));
            var paramId =Guid.NewGuid(); 
                     
             var result = await bankController.Update(paramId,updateBankCommand);

             var updatedResult = Assert.IsType<OkResult>(result);
             Assert.Equal(updatedResult.StatusCode,200);
             Assert.Equal(updateBankCommand.Id,paramId);
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<UpdateBankCommand>(), default), Times.Once);
        }
    }
}