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
    public class CreateBankAccountHandlersTests
    {
        public readonly Fixture _fixture ;
        public Mock<IUnitOFWork> _unitOfWorkMock { get; set; }
        public Mock<ILogger<CreateBankAccountHandler>> _loggerMock { get; set; }
        public CreateBankAccountHandlersTests()
        {
            _fixture = new Fixture();
                _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _fixture.Behaviors.Remove(b));
                    _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            _unitOfWorkMock = new Mock<IUnitOFWork>();
           
            _loggerMock = new Mock<ILogger<CreateBankAccountHandler>>();
        }
        
        [Trait("Handler", "CreateBankAccountHandler")]
        [Fact]
        public async void Handler_ValidCommand_CreateBankAccountSuccessful()
        {
                        
            var user = _fixture.Create<User>();
            var bank = _fixture.Create<Bank>();
            var createBankAccountCommand = _fixture.Create<CreateBankAccountCommand>();
            createBankAccountCommand.Type="Current";

            var cancellationToken = _fixture.Create<CancellationToken>();
            _unitOfWorkMock.Setup(uow => uow.userRepository.GetById(It.IsAny<Guid>())).ReturnsAsync(user);
            _unitOfWorkMock.Setup(uow => uow.bankRepository.GetById(It.IsAny<Guid>())).ReturnsAsync(bank);
            _unitOfWorkMock.Setup(uow => uow.bankAccountRepository.Add(It.IsAny<BankAccount>()));
            _unitOfWorkMock.Setup(uow => uow.userBanksAccountsRepository.Add(It.IsAny<UserBanksAccounts>()));
         
            var handler = new CreateBankAccountHandler(_unitOfWorkMock.Object, _loggerMock.Object);

           await handler.Handle(createBankAccountCommand, cancellationToken);

            _unitOfWorkMock.Verify(c => c.userRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.userRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.bankAccountRepository.Add(It.IsAny<BankAccount>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.userBanksAccountsRepository.Add(It.IsAny<UserBanksAccounts>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Once());
        }

         [Fact]
        public async Task Handler_UserNotFound_ReturnErrorUserNotFound()
        {      
            var user = _fixture.Create<User>();
            var bank = _fixture.Create<Bank>();
            var createBankAccountCommand = _fixture.Create<CreateBankAccountCommand>();
            createBankAccountCommand.Type="Current";

            var cancellationToken = _fixture.Create<CancellationToken>();
            _unitOfWorkMock.Setup(uow => uow.userRepository.GetById(It.IsAny<Guid>()));
            _unitOfWorkMock.Setup(uow => uow.bankRepository.GetById(It.IsAny<Guid>())).ReturnsAsync(bank);
            _unitOfWorkMock.Setup(uow => uow.bankAccountRepository.Add(It.IsAny<BankAccount>()));
            _unitOfWorkMock.Setup(uow => uow.userBanksAccountsRepository.Add(It.IsAny<UserBanksAccounts>()));
         
            var handler = new CreateBankAccountHandler(_unitOfWorkMock.Object, _loggerMock.Object);

           var exception = await Assert.ThrowsAsync<ApplicationException>(() => 
	                   handler.Handle(createBankAccountCommand, cancellationToken));

            Assert.Equal("User not found", exception.Message);
            _unitOfWorkMock.Verify(c => c.userRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.bankRepository.GetById(It.IsAny<Guid>()), Times.Never());
            _unitOfWorkMock.Verify(c => c.bankAccountRepository.Add(It.IsAny<BankAccount>()), Times.Never());
            _unitOfWorkMock.Verify(c => c.userBanksAccountsRepository.Add(It.IsAny<UserBanksAccounts>()), Times.Never());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Never());
        }

         [Fact]
        public async Task Handler_BankNotFound_ReturnErrorBankNotFound()
        {             
            var user = _fixture.Create<User>();
            var bank = _fixture.Create<Bank>();
            var createBankAccountCommand = _fixture.Create<CreateBankAccountCommand>();
            createBankAccountCommand.Type="Current";

            var cancellationToken = _fixture.Create<CancellationToken>();
            _unitOfWorkMock.Setup(uow => uow.userRepository.GetById(It.IsAny<Guid>())).ReturnsAsync(user);
            _unitOfWorkMock.Setup(uow => uow.bankRepository.GetById(It.IsAny<Guid>()));
            _unitOfWorkMock.Setup(uow => uow.bankAccountRepository.Add(It.IsAny<BankAccount>()));
            _unitOfWorkMock.Setup(uow => uow.userBanksAccountsRepository.Add(It.IsAny<UserBanksAccounts>()));
         
            var handler = new CreateBankAccountHandler(_unitOfWorkMock.Object, _loggerMock.Object);

           var exception = await Assert.ThrowsAsync<ApplicationException>(() => 
	                   handler.Handle(createBankAccountCommand, cancellationToken));

            Assert.Equal("Banck not found", exception.Message);

            _unitOfWorkMock.Verify(c => c.userRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.bankRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.bankAccountRepository.Add(It.IsAny<BankAccount>()), Times.Never());
            _unitOfWorkMock.Verify(c => c.userBanksAccountsRepository.Add(It.IsAny<UserBanksAccounts>()), Times.Never());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Never());
        }
    }
}