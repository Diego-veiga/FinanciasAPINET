using AutoFixture;
using financias.src.interfaces;
using financiasapi.src.commands.Bank;
using financiasapi.src.handlers.BankDelete;
using financiasapi.src.models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace financiastests.handlers.Banks
{
    public class DeleteBankHandlersTests
    {
        public readonly Fixture _fixture ;
        public Mock<IUnitOFWork> _unitOfWorkMock { get; set; }
        public Mock<ILogger<DeleteBankHandler>> _loggerMock { get; set; }

        public DeleteBankHandlersTests()
        {
            _fixture = new Fixture();
                _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _fixture.Behaviors.Remove(b));
                    _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            _unitOfWorkMock = new Mock<IUnitOFWork>();
           
            _loggerMock = new Mock<ILogger<DeleteBankHandler>>();
        }

       [Fact]
        public async void  Handler_ValidCommand_DeleteBankSuccessful()
        {
            var bank = _fixture.Create<Bank>();
            var deleteBankCommand = _fixture.Create<DeleteBankCommand>();

            var cancellationToken = _fixture.Create<CancellationToken>();
            _unitOfWorkMock.Setup(uow => uow.bankRepository.GetById(It.IsAny<Guid>())).ReturnsAsync(bank);
            _unitOfWorkMock.Setup(uow => uow.bankRepository.Delete(It.IsAny<Bank>()));

            var handler = new DeleteBankHandler(_unitOfWorkMock.Object, _loggerMock.Object);

            await handler.Handle(deleteBankCommand, cancellationToken);

            _unitOfWorkMock.Verify(c => c.bankRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.bankRepository.Delete(It.IsAny<Bank>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Once());
        }

        [Fact]
        public async void Handler_BankNotFound_ReturnApplicationException()
        {              
            var deleteBankCommand = _fixture.Create<DeleteBankCommand>();
           
            var cancellationToken = _fixture.Create<CancellationToken>();
            _unitOfWorkMock.Setup(uow => uow.bankRepository.GetById(It.IsAny<Guid>()));
    
            var handler = new DeleteBankHandler(_unitOfWorkMock.Object, _loggerMock.Object);

           var exception = await Assert.ThrowsAsync<ApplicationException>(() => 
	                   handler.Handle(deleteBankCommand, cancellationToken));

            Assert.Equal("Bank not found", exception.Message);

            _unitOfWorkMock.Verify(c => c.bankRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.bankRepository.Update(It.IsAny<Bank>()), Times.Never());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Never());
        }

        [Fact]
        public async void Handler_DefaultBankNotFound_ReturnApplicationException()
        {              
            var deleteBankCommand = _fixture.Create<DeleteBankCommand>();
            var bank = _fixture.Create<Bank>();
            bank.UserId = null;
           
            var cancellationToken = _fixture.Create<CancellationToken>();
            _unitOfWorkMock.Setup(uow => uow.bankRepository.GetById(It.IsAny<Guid>())).ReturnsAsync(bank);
    
            var handler = new DeleteBankHandler(_unitOfWorkMock.Object, _loggerMock.Object);

           var exception = await Assert.ThrowsAsync<ApplicationException>(() => 
	                   handler.Handle(deleteBankCommand, cancellationToken));

            Assert.Equal("Default bank can't deleted", exception.Message);

            _unitOfWorkMock.Verify(c => c.bankRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.bankRepository.Update(It.IsAny<Bank>()), Times.Never());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Never());
        }
    }
}