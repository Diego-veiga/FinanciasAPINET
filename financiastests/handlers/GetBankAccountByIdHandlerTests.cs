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
    public class GetBankAccountByIdHandlerTests
    {
        public readonly Fixture _fixture ;
        public Mock<IUnitOFWork> _unitOfWorkMock { get; set; }
        public Mock<IMapper> _mapperMock{ get; set; }
        public Mock<ILogger<GetBankAccountAllByUserIdHandler>> _loggerMock { get; set; }

        public GetBankAccountByIdHandlerTests()
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
        public void Handler_BankAccountExists_ReturnBankAccountView()
        {
            var bankAccounts = _fixture.Create<BankAccount>();
            
            var getBankAccountByIdRequest = _fixture.Create<GetBankAccountById>();
           
            _unitOfWorkMock.Setup(uow => uow.bankAccountRepository.GetById(It.IsAny<Guid>())).ReturnsAsync(bankAccounts);
            _mapperMock.Setup(mm => mm.Map<BankAccountView>(It.IsAny<BankAccount>())).Returns(new BankAccountView());

            var handler = new GetBankAccountByIdHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object);
            var result = handler.Handle(getBankAccountByIdRequest, CancellationToken.None);


            _unitOfWorkMock.Verify(c => c.bankAccountRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _mapperMock.Verify(mm => mm.Map<BankAccountView>(It.IsAny<BankAccount>()), Times.Once());
            Assert.IsType<BankAccountView>(result.Result);
        }

        [Fact]
        public void Handler_BankAccountNotFound_ReturnNull()
        {            
            var getBankAccountByIdRequest = _fixture.Create<GetBankAccountById>();
           
            _unitOfWorkMock.Setup(uow => uow.bankAccountRepository.GetById(It.IsAny<Guid>()));
            _mapperMock.Setup(mm => mm.Map<BankAccountView>(It.IsAny<BankAccount>()));

            var handler = new GetBankAccountByIdHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object);
            var result = handler.Handle(getBankAccountByIdRequest, CancellationToken.None);


            _unitOfWorkMock.Verify(c => c.bankAccountRepository.GetById(It.IsAny<Guid>()), Times.Once());
            _mapperMock.Verify(mm => mm.Map<BankAccountView>(It.IsAny<BankAccount>()), Times.Never());
            Assert.Equal(result.Result, null);
        }
    }
}