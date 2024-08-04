using AutoFixture;
using AutoMapper;
using financias.src.handlers;
using financias.src.interfaces;
using financias.src.query.BankAccount;
using financiasapi.src.dtos;
using financiasapi.src.models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace financiastests.handlers
{
    public class GetBankAccountAllByUserIdHandlerTests
    {
        public readonly Fixture _fixture ;
        public Mock<IUnitOFWork> _unitOfWorkMock { get; set; }
        public Mock<IMapper> _mapperMock{ get; set; }
        public Mock<ILogger<GetBankAccountAllByUserIdHandler>> _loggerMock { get; set; }

        public GetBankAccountAllByUserIdHandlerTests()
        {
            _fixture = new Fixture();
                _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _fixture.Behaviors.Remove(b));
                    _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            _unitOfWorkMock = new Mock<IUnitOFWork>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<GetBankAccountAllByUserIdHandler>>(); 
        }
        
        [Fact]
        public void Handler_ValidCommand_ReturnListBankAccountView()
        {
            var bankAccounts = _fixture.Create<List<BankAccount>>();
            
            var getBankAccountAllByUserIdRequest = _fixture.Create<GetBankAccountAllByUserId>();
           
            _unitOfWorkMock.Setup(uow => uow.bankAccountRepository.GetByUserId(It.IsAny<Guid>())).ReturnsAsync(bankAccounts);
            _mapperMock.Setup(mm => mm.Map<BankAccountView>(It.IsAny<BankAccount>()));

            var handler = new GetBankAccountAllByUserIdHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object);
            var result = handler.Handle(getBankAccountAllByUserIdRequest,CancellationToken.None);


            _unitOfWorkMock.Verify(c => c.bankAccountRepository.GetByUserId(It.IsAny<Guid>()), Times.Once());
            _mapperMock.Verify(mm => mm.Map<BankAccountView>(It.IsAny<BankAccount>()), Times.Exactly(bankAccounts.Count));
            Assert.Equal(result.Result.Count,bankAccounts.Count);
            Assert.IsType<List<BankAccountView>>(result.Result);
        }

        [Fact]
        public void Handler_NotFoundBankAccountWithUserId_ReturnEmptyList()
        {            
            var getBankAccountAllByUserIdRequest = _fixture.Create<GetBankAccountAllByUserId>();
           
            _unitOfWorkMock.Setup(uow => uow.bankAccountRepository.GetByUserId(It.IsAny<Guid>())).ReturnsAsync(new List<BankAccount>());
            _mapperMock.Setup(mm => mm.Map<BankAccountView>(It.IsAny<BankAccount>()));

            var handler = new GetBankAccountAllByUserIdHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object);
            var result = handler.Handle(getBankAccountAllByUserIdRequest,CancellationToken.None);


            _unitOfWorkMock.Verify(c => c.bankAccountRepository.GetByUserId(It.IsAny<Guid>()), Times.Once());
            _mapperMock.Verify(mm => mm.Map<BankAccountView>(It.IsAny<BankAccount>()), Times.Never());
            Assert.Equal(result.Result.Count,0);
            Assert.IsType<List<BankAccountView>>(result.Result);
        }
    }
}