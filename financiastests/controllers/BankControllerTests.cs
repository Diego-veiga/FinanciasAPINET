using System.Security.Claims;
using AutoFixture;
using financias.src.commands.BankAccount;
using financias.src.controllers;
using financias.src.interfaces;
using financiasapi.src.commands.Bank;
using financiasapi.src.dtos;
using financiasapi.src.query.Bank;
using financiasapi.src.query.Banks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using DeleteBankCommand = financiasapi.src.commands.Bank.DeleteBankCommand;

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

        [Fact]
        public async void Delete_ValidParam_SendCommandProcess()
        {
            var bankController = new BankController(_mediatorMock.Object, _bankServiceMock.Object, _loggerMock.Object);
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteBankCommand>(), default)).Returns(Task.CompletedTask);

            var result = await bankController.Delete(Guid.NewGuid());

            var deletedResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(deletedResult.StatusCode, 200);
            Assert.Equal(deletedResult.Value, "Bank deleted success");
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<DeleteBankCommand>(), default), Times.Once);
        }

        [Fact]
        public async void GetById_ValidParam_SendCommandProcess()
        {
            var bankController = new BankController(_mediatorMock.Object, _bankServiceMock.Object, _loggerMock.Object);
            var bankView = _fixture.Create<BankView>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBankById>(), default)).ReturnsAsync(bankView);

            var result = await bankController.GetById(Guid.NewGuid());

            var getByIdResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(getByIdResult.StatusCode, 200);
            Assert.Equal(getByIdResult.Value, bankView);
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<GetBankById>(), default), Times.Once);
        }

        [Fact]
        public async void GetActive_ValidParam_SendCommandProcess()
        {
            var bankController = new BankController(_mediatorMock.Object, _bankServiceMock.Object, _loggerMock.Object);
            var bankViews = _fixture.Create<List<BankView>>();
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
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBankAllByUserId>(), default)).ReturnsAsync(bankViews);

            var result = await bankController.GetActive();

            var getActiveResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(getActiveResult.StatusCode, 200);
            Assert.Equal(getActiveResult.Value, bankViews);
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<GetBankAllByUserId>(), default), Times.Once);
        }

        [Fact]
        public async void GetByUserId_ValidParam_SendCommandProcess()
        {
            var bankController = new BankController(_mediatorMock.Object, _bankServiceMock.Object, _loggerMock.Object);
            var bankViews = _fixture.Create<List<BankView>>();
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
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBankByUserId>(), default)).ReturnsAsync(bankViews);

            var result = await bankController.GetByUserId(Guid.NewGuid());

            var getByUserIdResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(getByUserIdResult.StatusCode, 200);
            Assert.Equal(getByUserIdResult.Value, bankViews);
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<GetBankByUserId>(), default), Times.Once);
        }
    }
}