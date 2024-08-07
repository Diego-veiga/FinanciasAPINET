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
    public class DeleteBankAccountHandlerTests
    {
         public readonly Fixture _fixture ;
         public Mock<IUnitOFWork> _unitOfWorkMock { get; set; }
        public Mock<ILogger<DeleteBankAccountHandler>> _loggerMock { get; set; }

         public DeleteBankAccountHandlerTests()
        {
            _fixture = new Fixture();
                _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _fixture.Behaviors.Remove(b));
                    _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

            _unitOfWorkMock = new Mock<IUnitOFWork>();  
            _loggerMock = new Mock<ILogger<DeleteBankAccountHandler>>();
        }

        [Trait("Handler", "DeleteBankAccountHandler")]
        [Fact]
        public async void Handler_ExistingBankAccount_SuccessfullyDisablesBankAccount ()
        {
            var bankAccount = _fixture.Create<BankAccount>();
            bankAccount.UpdatedAt= bankAccount.CreatedAt;
            var deleteBankAccountCommand = _fixture.Create<DeleteBankAccountCommand>();
            var cancellationToken = _fixture.Create<CancellationToken>();
            _unitOfWorkMock.Setup(uow => uow.bankAccountRepository.GetById(It.IsAny<Guid>())).ReturnsAsync(bankAccount);
            _unitOfWorkMock.Setup(uow => uow.bankAccountRepository.Delete(It.IsAny<BankAccount>()));
            _unitOfWorkMock.Setup(uow => uow.Commit());

            var handler = new DeleteBankAccountHandler(_unitOfWorkMock.Object, _loggerMock.Object);

            await handler.Handle(deleteBankAccountCommand, cancellationToken);

              Assert.Equal(bankAccount.Active,false);
              Assert.NotEqual(bankAccount.UpdatedAt, bankAccount.CreatedAt);
             _unitOfWorkMock.Verify(uow => uow.bankAccountRepository.GetById(It.IsAny<Guid>()),Times.Once());
             _unitOfWorkMock.Verify(uow => uow.bankAccountRepository.Delete(It.IsAny<BankAccount>()),Times.Once());
             _unitOfWorkMock.Verify(uow => uow.Commit(),Times.Once());
        }

        [Fact]
        public async void Handler_NotFoundBankAccount_ThrowExceptionBankAccountNotFound()
        {
            var bankAccount = _fixture.Create<BankAccount>();
            var deleteBankAccountCommand = _fixture.Create<DeleteBankAccountCommand>();
            var cancellationToken = _fixture.Create<CancellationToken>();
            _unitOfWorkMock.Setup(uow => uow.bankAccountRepository.GetById(It.IsAny<Guid>()));
            _unitOfWorkMock.Setup(uow => uow.bankAccountRepository.Delete(It.IsAny<BankAccount>()));
            _unitOfWorkMock.Setup(uow => uow.Commit());

            var handler = new DeleteBankAccountHandler(_unitOfWorkMock.Object, _loggerMock.Object);

            var exception = await Assert.ThrowsAsync<ApplicationException>(() => 
	                   handler.Handle(deleteBankAccountCommand, cancellationToken));

            Assert.Equal("BankAccount not found", exception.Message);

            _unitOfWorkMock.Verify(uow => uow.bankAccountRepository.GetById(It.IsAny<Guid>()),Times.Once());
            _unitOfWorkMock.Verify(uow => uow.bankAccountRepository.Delete(It.IsAny<BankAccount>()),Times.Never());
            _unitOfWorkMock.Verify(uow => uow.Commit(),Times.Never());
        }
    }
}