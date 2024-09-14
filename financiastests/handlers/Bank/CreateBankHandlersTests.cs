using AutoFixture;
using financias.src.interfaces;
using financiasapi.src.commands.Bank;
using financiasapi.src.handlers.BankHandler;
using financiasapi.src.models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace financiastests.handlers.Banks
{
    public class CreateBankHandlersTests
    {
         public readonly Fixture _fixture ;
        public Mock<IUnitOFWork> _unitOfWorkMock { get; set; }
        public Mock<ILogger<CreateBankHandler>> _loggerMock { get; set; }

        public CreateBankHandlersTests()
        {
            _fixture = new Fixture();
                _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _fixture.Behaviors.Remove(b));
                    _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            _unitOfWorkMock = new Mock<IUnitOFWork>();
           
            _loggerMock = new Mock<ILogger<CreateBankHandler>>();
        }
        
        [Fact]
        public async void Handler_ValidCommand_CreateBankSuccessful()
        {
                        
            var user = _fixture.Create<User>();
            var bank = _fixture.Create<Bank>();
            var createBankAccountCommand = _fixture.Create<CreateBankCommand>();
           

            var cancellationToken = _fixture.Create<CancellationToken>();
            _unitOfWorkMock.Setup(uow => uow.userRepository.GetById(It.IsAny<Guid>())).ReturnsAsync(user);
            _unitOfWorkMock.Setup(uow => uow.bankRepository.Add(It.IsAny<Bank>()));
                     
            var handler = new CreateBankHandler(_unitOfWorkMock.Object, _loggerMock.Object);

           await handler.Handle(createBankAccountCommand, cancellationToken);

            _unitOfWorkMock.Verify(c => c.userRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.bankRepository.Add(It.IsAny<Bank>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Once());
        }

        [Fact]
        public async void Handler_UserNotFound_ReturnApplicationException()
        {
                        
            var user = _fixture.Create<User>();
            var bank = _fixture.Create<Bank>();
            var createBankAccountCommand = _fixture.Create<CreateBankCommand>();
           

            var cancellationToken = _fixture.Create<CancellationToken>();
            _unitOfWorkMock.Setup(uow => uow.userRepository.GetById(It.IsAny<Guid>()));
            _unitOfWorkMock.Setup(uow => uow.bankRepository.Add(It.IsAny<Bank>()));
                     
            var handler = new CreateBankHandler(_unitOfWorkMock.Object, _loggerMock.Object);

           var exception = await Assert.ThrowsAsync<ApplicationException>(() => 
	                   handler.Handle(createBankAccountCommand, cancellationToken));

            Assert.Equal("User not found", exception.Message);

            _unitOfWorkMock.Verify(c => c.userRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.bankRepository.Add(It.IsAny<Bank>()), Times.Never());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Never());
        }
    }  
}
