using AutoFixture;
using financias.src.commands.BankAccount;
using financias.src.handlers;
using financias.src.interfaces;
using financiasapi.src.models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace financiastests.handlers
{
    public class UpdateBankAccountHandlerTests
    {
         public readonly Fixture _fixture ;
        public Mock<IUnitOFWork> _unitOfWorkMock { get; set; }
        public Mock<ILogger<UpdateBankAccountHandler>> _loggerMock { get; set; }

        public UpdateBankAccountHandlerTests()
        {
             _fixture = new Fixture();
                _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _fixture.Behaviors.Remove(b));
                    _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            _unitOfWorkMock = new Mock<IUnitOFWork>();
           
            _loggerMock = new Mock<ILogger<UpdateBankAccountHandler>>();
            
        }

        [Fact]
        public void Handler_ValidCommand_UpdateBankAccountSuccessful()
        {
            var bank = _fixture.Create<Bank>();
            var bankAccount = _fixture.Create<BankAccount>();
            var cancellationToken = _fixture.Create<CancellationToken>();
            var updateBankAccountCommand = _fixture.Create<UpdateBankAccountCommand>();
            updateBankAccountCommand.Type="Current";
            _unitOfWorkMock.Setup(uow => uow.bankAccountRepository.GetById(It.IsAny<Guid>())).ReturnsAsync(bankAccount);
            _unitOfWorkMock.Setup(uow => uow.bankRepository.GetById(It.IsAny<Guid>())).ReturnsAsync(bank);
            _unitOfWorkMock.Setup(uow => uow.bankAccountRepository.Update(It.IsAny<BankAccount>()));
            _unitOfWorkMock.Setup(uow => uow.Commit());


             var handler = new UpdateBankAccountHandler(_unitOfWorkMock.Object, _loggerMock.Object); 
             handler.Handle(updateBankAccountCommand,cancellationToken);
            
            
            _unitOfWorkMock.Verify(c => c.bankAccountRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.bankRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.bankAccountRepository.Update(It.IsAny<BankAccount>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Once());
   
        }

        [Fact]
        public async void Handler_BankAccountNotFound_ReturnErrorBankAccountNotFound()
        {
            var cancellationToken = _fixture.Create<CancellationToken>();
            var updateBankAccountCommand = _fixture.Create<UpdateBankAccountCommand>();
            updateBankAccountCommand.Type="Current";
            _unitOfWorkMock.Setup(uow => uow.bankAccountRepository.GetById(It.IsAny<Guid>()));
            _unitOfWorkMock.Setup(uow => uow.bankRepository.GetById(It.IsAny<Guid>()));
            _unitOfWorkMock.Setup(uow => uow.bankAccountRepository.Update(It.IsAny<BankAccount>()));
            _unitOfWorkMock.Setup(uow => uow.Commit());


             var handler = new UpdateBankAccountHandler(_unitOfWorkMock.Object, _loggerMock.Object); 
            var exception = await Assert.ThrowsAsync<ApplicationException>(() => 
	                   handler.Handle(updateBankAccountCommand, cancellationToken));
            
            
            _unitOfWorkMock.Verify(c => c.bankAccountRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.bankRepository.GetById(It.IsAny<Guid>()), Times.Never());
            _unitOfWorkMock.Verify(c => c.bankAccountRepository.Update(It.IsAny<BankAccount>()), Times.Never());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Never());
        }

        [Fact]
        public async void Handler_BankNotFound_ReturnErrorBankNotFound()
        {
            var bankAccount = _fixture.Create<BankAccount>();
            var cancellationToken = _fixture.Create<CancellationToken>();
            var updateBankAccountCommand = _fixture.Create<UpdateBankAccountCommand>();
            updateBankAccountCommand.Type="Current";
            _unitOfWorkMock.Setup(uow => uow.bankAccountRepository.GetById(It.IsAny<Guid>())).ReturnsAsync(bankAccount);
            _unitOfWorkMock.Setup(uow => uow.bankRepository.GetById(It.IsAny<Guid>()));
            _unitOfWorkMock.Setup(uow => uow.bankAccountRepository.Update(It.IsAny<BankAccount>()));
            _unitOfWorkMock.Setup(uow => uow.Commit());


             var handler = new UpdateBankAccountHandler(_unitOfWorkMock.Object, _loggerMock.Object); 
            var exception = await Assert.ThrowsAsync<ApplicationException>(() => 
	                   handler.Handle(updateBankAccountCommand, cancellationToken));
            
            
            _unitOfWorkMock.Verify(c => c.bankAccountRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.bankRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.bankAccountRepository.Update(It.IsAny<BankAccount>()), Times.Never());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Never());
        }
    }
}