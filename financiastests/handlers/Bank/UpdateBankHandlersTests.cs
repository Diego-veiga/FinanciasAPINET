using AutoFixture;
using financias.src.interfaces;
using financiasapi.src.commands.Bank;
using financiasapi.src.handlers.BankUpdate;
using financiasapi.src.models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace financiastests.handlers.Banks
{
    public class UpdateBankHandlersTests
    {
        public readonly Fixture _fixture ;
        public Mock<IUnitOFWork> _unitOfWorkMock { get; set; }
        public Mock<ILogger<UpdateCommandHandler>> _loggerMock { get; set; }

        public UpdateBankHandlersTests()
        {
            _fixture = new Fixture();
                _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _fixture.Behaviors.Remove(b));
                    _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            _unitOfWorkMock = new Mock<IUnitOFWork>();
           
            _loggerMock = new Mock<ILogger<UpdateCommandHandler>>();
        }
        
        [Fact]
        public async void  Handler_ValidCommand_UpdateBankSuccessful()
        {
            var bank = _fixture.Create<Bank>();
            var updateBankCommand = _fixture.Create<UpdateBankCommand>();

            var cancellationToken = _fixture.Create<CancellationToken>();
            _unitOfWorkMock.Setup(uow => uow.bankRepository.GetById(It.IsAny<Guid>())).ReturnsAsync(bank);
            _unitOfWorkMock.Setup(uow => uow.bankRepository.Update(It.IsAny<Bank>()));

            var handler = new UpdateCommandHandler(_unitOfWorkMock.Object, _loggerMock.Object);

            await handler.Handle(updateBankCommand, cancellationToken);

            _unitOfWorkMock.Verify(c => c.bankRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.bankRepository.Update(It.IsAny<Bank>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Once());
        }

        [Fact]
        public async void Handler_BankNotFound_ReturnApplicationException()
        {              
            var updateBankCommand = _fixture.Create<UpdateBankCommand>();
           
            var cancellationToken = _fixture.Create<CancellationToken>();
            _unitOfWorkMock.Setup(uow => uow.bankRepository.GetById(It.IsAny<Guid>()));
    
            var handler = new UpdateCommandHandler(_unitOfWorkMock.Object, _loggerMock.Object);

           var exception = await Assert.ThrowsAsync<ApplicationException>(() => 
	                   handler.Handle(updateBankCommand, cancellationToken));

            Assert.Equal("Bank not found", exception.Message);

            _unitOfWorkMock.Verify(c => c.bankRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.bankRepository.Update(It.IsAny<Bank>()), Times.Never());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Never());
        }
    }
}