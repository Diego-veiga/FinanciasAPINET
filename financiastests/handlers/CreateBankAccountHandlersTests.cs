
using AutoFixture;
using financias.src.commands.BanckAccount;
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
        public readonly Fixture fixture ;
        public Mock<IUnitOFWork> _unitOfWorkMock { get; set; }
        public Mock<ILogger<CreateBanckAccountHandler>> loggerMock { get; set; }
        public CreateBankAccountHandlersTests()
        {
            fixture = new Fixture();
                fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => fixture.Behaviors.Remove(b));
                    fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            _unitOfWorkMock = new Mock<IUnitOFWork>();
           
            loggerMock = new Mock<ILogger<CreateBanckAccountHandler>>();
        }

        [Fact]
        public async void Handler_ValidCommand_CreateBankAccountSuccessful()
        {
                        
            var user = fixture.Create<User>();
            var bank = fixture.Create<Bank>();
            var createBankAccountCommand = fixture.Create<CreateBanckAcconutCommand>();
            createBankAccountCommand.Type="Current";

            var cancellationToken = fixture.Create<CancellationToken>();
            _unitOfWorkMock.Setup(uow => uow.userRepository.GetById(It.IsAny<Guid>())).ReturnsAsync(user);
            _unitOfWorkMock.Setup(uow => uow.bankRepository.GetById(It.IsAny<Guid>())).ReturnsAsync(bank);
            _unitOfWorkMock.Setup(uow => uow.banckAccountRepository.Add(It.IsAny<BanckAccount>()));
            _unitOfWorkMock.Setup(uow => uow.userBancksAccountsRepository.Add(It.IsAny<UserBancksAccounts>()));
         
            var handler = new CreateBanckAccountHandler(_unitOfWorkMock.Object, loggerMock.Object);

           await handler.Handle(createBankAccountCommand, cancellationToken);

            _unitOfWorkMock.Verify(c => c.userRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.userRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.banckAccountRepository.Add(It.IsAny<BanckAccount>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.userBancksAccountsRepository.Add(It.IsAny<UserBancksAccounts>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Once());
        }

         [Fact]
        public async Task Handler_UserNotFound_ReturnErrorUserNotFound()
        {
                        
            var user = fixture.Create<User>();
            var bank = fixture.Create<Bank>();
            var createBankAccountCommand = fixture.Create<CreateBanckAcconutCommand>();
            createBankAccountCommand.Type="Current";

            var cancellationToken = fixture.Create<CancellationToken>();
            _unitOfWorkMock.Setup(uow => uow.userRepository.GetById(It.IsAny<Guid>()));
            _unitOfWorkMock.Setup(uow => uow.bankRepository.GetById(It.IsAny<Guid>())).ReturnsAsync(bank);
            _unitOfWorkMock.Setup(uow => uow.banckAccountRepository.Add(It.IsAny<BanckAccount>()));
            _unitOfWorkMock.Setup(uow => uow.userBancksAccountsRepository.Add(It.IsAny<UserBancksAccounts>()));
         
            var handler = new CreateBanckAccountHandler(_unitOfWorkMock.Object, loggerMock.Object);

           var exception = await Assert.ThrowsAsync<ApplicationException>(() => 
	                   handler.Handle(createBankAccountCommand, cancellationToken));

            Assert.Equal("User not found", exception.Message);

            _unitOfWorkMock.Verify(c => c.userRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.bankRepository.GetById(It.IsAny<Guid>()), Times.Never());
            _unitOfWorkMock.Verify(c => c.banckAccountRepository.Add(It.IsAny<BanckAccount>()), Times.Never());
            _unitOfWorkMock.Verify(c => c.userBancksAccountsRepository.Add(It.IsAny<UserBancksAccounts>()), Times.Never());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Never());
        }

         [Fact]
        public async Task Handler_BankNotFound_ReturnErrorBankNotFound()
        {             
            var user = fixture.Create<User>();
            var bank = fixture.Create<Bank>();
            var createBankAccountCommand = fixture.Create<CreateBanckAcconutCommand>();
            createBankAccountCommand.Type="Current";

            var cancellationToken = fixture.Create<CancellationToken>();
            _unitOfWorkMock.Setup(uow => uow.userRepository.GetById(It.IsAny<Guid>())).ReturnsAsync(user);
            _unitOfWorkMock.Setup(uow => uow.bankRepository.GetById(It.IsAny<Guid>()));
            _unitOfWorkMock.Setup(uow => uow.banckAccountRepository.Add(It.IsAny<BanckAccount>()));
            _unitOfWorkMock.Setup(uow => uow.userBancksAccountsRepository.Add(It.IsAny<UserBancksAccounts>()));
         
            var handler = new CreateBanckAccountHandler(_unitOfWorkMock.Object, loggerMock.Object);

           var exception = await Assert.ThrowsAsync<ApplicationException>(() => 
	                   handler.Handle(createBankAccountCommand, cancellationToken));

            Assert.Equal("Banck not found", exception.Message);

            _unitOfWorkMock.Verify(c => c.userRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.bankRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.banckAccountRepository.Add(It.IsAny<BanckAccount>()), Times.Never());
            _unitOfWorkMock.Verify(c => c.userBancksAccountsRepository.Add(It.IsAny<UserBancksAccounts>()), Times.Never());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Never());
        }
    }
}