using System.Security.Claims;
using AutoFixture;
using financias.src.commands.BankAccount;
using financias.src.controllers;
using financias.src.query.BankAccount;
using financiasapi.src.dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace financiastests.controllers
{
    public class BankAccountControllerTest
    {
        private  Mock<IMediator> _mediatorMock;
        private Mock<ILogger<BankAccountController>> _loggerMock;
        public readonly Fixture _fixture ;

        public BankAccountControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<BankAccountController>>();
             _fixture = new Fixture();
                _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _fixture.Behaviors.Remove(b));
                    _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
        }

        [Fact]
        public async void Create_ValidCreateBankAccountCommand_SendCommandProcess()
        {
            var bankAccountController = new BankAccountController(_mediatorMock.Object,_loggerMock.Object);
            var claims = new List<Claim>
            {
                new Claim("id", Guid.NewGuid().ToString())
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            bankAccountController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

            var createBankAccountCommand = _fixture.Create<CreateBankAccountCommand>();
           _mediatorMock.Setup(m => m.Send(It.IsAny<CreateBankAccountCommand>(),default))
                     .Returns(Task.CompletedTask);

            var result = await bankAccountController.Create(createBankAccountCommand);

             var createdResult = Assert.IsType<CreatedAtRouteResult>(result);
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<CreateBankAccountCommand>(), default), Times.Once);
        }

        [Fact]
        public async void Update_ValidUpdateBankAccountCommand_SendCommandProcess()
        {
            var bankAccountController = new BankAccountController(_mediatorMock.Object,_loggerMock.Object);
            var claims = new List<Claim>
            {
                new Claim("id", Guid.NewGuid().ToString())
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            bankAccountController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

            var updateBankAccountCommand = _fixture.Create<UpdateBankAccountCommand>();
           _mediatorMock.Setup(m => m.Send(It.IsAny<CreateBankAccountCommand>(),default))
                     .Returns(Task.CompletedTask);

            var result = await bankAccountController.Update(updateBankAccountCommand, Guid.NewGuid());

             var updatedResult = Assert.IsType<OkResult>(result);
             Assert.Equal(updatedResult.StatusCode,200);
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<UpdateBankAccountCommand>(), default), Times.Once);
        }

        [Fact]
        public async void Delete_ValidParam_SendCommandProcess()
        {
            var bankAccountController = new BankAccountController(_mediatorMock.Object,_loggerMock.Object);
            var claims = new List<Claim>
            {
                new Claim("id", Guid.NewGuid().ToString())
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            bankAccountController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

            var deleteBankAccountCommand = _fixture.Create<DeleteBankAccountCommand>();
           _mediatorMock.Setup(m => m.Send(It.IsAny<CreateBankAccountCommand>(),default))
                     .Returns(Task.CompletedTask);

            var result = await bankAccountController.Delete(Guid.NewGuid());

             var deletedResult = Assert.IsType<NoContentResult>(result);
             Assert.Equal(deletedResult.StatusCode,204);
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<DeleteBankAccountCommand>(), default), Times.Once);
        }

        [Fact]
        public async void GetById_ValidParam_ReturnObject()
        {
            var bankAccountController = new BankAccountController(_mediatorMock.Object,_loggerMock.Object);
            var claims = new List<Claim>
            {
                new Claim("id", Guid.NewGuid().ToString())
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            bankAccountController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

            var getBankAccountById = _fixture.Create<GetBankAccountById>();
            var bankAccountView = _fixture.Create<BankAccountView>();
           _mediatorMock.Setup(m => m.Send(It.IsAny<GetBankAccountById>(),default))
                     .ReturnsAsync(bankAccountView);

             var result = await bankAccountController.GetById(Guid.NewGuid());
             
             var getByIdBankAccount = Assert.IsType<OkObjectResult>(result);
             Assert.Equal(getByIdBankAccount.StatusCode,200);
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<GetBankAccountById>(), default), Times.Once);
        }

        [Fact]
        public async void GetById_notFoundObject_ReturnNoFound()
        {
            var bankAccountController = new BankAccountController(_mediatorMock.Object,_loggerMock.Object);
            var claims = new List<Claim>
            {
                new Claim("id", Guid.NewGuid().ToString())
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            bankAccountController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

            var getBankAccountById = _fixture.Create<GetBankAccountById>();
           
           _mediatorMock.Setup(m => m.Send(It.IsAny<GetBankAccountById>(),default))
                     .ReturnsAsync(null as BankAccountView );

             var result = await bankAccountController.GetById(Guid.NewGuid());
             
             var getByIdBankAccount = Assert.IsType<NotFoundObjectResult>(result);
             Assert.Equal(getByIdBankAccount.StatusCode,404);
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<GetBankAccountById>(), default), Times.Once);
        }

        [Fact]
        public async void GetAll_ExistObject_ReturnListObject()
        {
            var bankAccountController = new BankAccountController(_mediatorMock.Object,_loggerMock.Object);
            var claims = new List<Claim>
            {
                new Claim("id", Guid.NewGuid().ToString())
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            bankAccountController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

            var getBankAccountAllByUserId = _fixture.Create<GetBankAccountAllByUserId>();
            var bankAccountViewList = _fixture.Create<List<BankAccountView>>();
           _mediatorMock.Setup(m => m.Send(It.IsAny<GetBankAccountAllByUserId>(),default))
                     .ReturnsAsync(bankAccountViewList);

             var result = await bankAccountController.GetAll();
             
             var getByIdBankAccount = Assert.IsType<OkObjectResult>(result);
             Assert.Equal(getByIdBankAccount.StatusCode,200);
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<GetBankAccountAllByUserId>(), default), Times.Once);
        }

         [Fact]
        public async void GetAll_notFoundObject_ReturnNoFound()
        {
            var bankAccountController = new BankAccountController(_mediatorMock.Object,_loggerMock.Object);
            var claims = new List<Claim>
            {
                new Claim("id", Guid.NewGuid().ToString())
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            bankAccountController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

            var getBankAccountAllByUserId = _fixture.Create<GetBankAccountAllByUserId>();
            var bankAccountViewList = _fixture.Create<List<BankAccountView>>();
           _mediatorMock.Setup(m => m.Send(It.IsAny<GetBankAccountAllByUserId>(),default))
                     .ReturnsAsync(null as List<BankAccountView>);

             var result = await bankAccountController.GetAll();
             
             var getByIdBankAccount = Assert.IsType<NotFoundObjectResult>(result);
             Assert.Equal(getByIdBankAccount.StatusCode, 404);
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<GetBankAccountAllByUserId>(), default), Times.Once);
        }


        
    }
}