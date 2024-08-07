using System.Text.Json;
using AutoMapper;
using financias.src.interfaces;
using financias.src.query.BankAccount;
using financiasapi.src.dtos;
using MediatR;

namespace financias.src.handlers
{
    public class GetBankAccountByIdHandler : IRequestHandler<GetBankAccountById, BankAccountView>
    {
        private IUnitOFWork _unitOFWork;
        private readonly IMapper _mapper;
        public ILogger<GetBankAccountAllByUserIdHandler> _logger { get; set; }

        public GetBankAccountByIdHandler(IUnitOFWork unitOFWork, IMapper mapper,ILogger<GetBankAccountAllByUserIdHandler> logger)
        {
            _unitOFWork = unitOFWork;
            _mapper = mapper;
            _logger=logger;
        }

        public async Task<BankAccountView> Handle(GetBankAccountById request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start GetBankAccountByIdHandler with request {JsonSerializer.Serialize(request)}");

            var bankAccount = await _unitOFWork.bankAccountRepository.GetById(request.Id);

            _logger.LogInformation($"Return result bankAccountRepository.GetById {JsonSerializer.Serialize(bankAccount)}");

            if (bankAccount is null)
            {
                 _logger.LogInformation($"Returning bankAccount null");

                return null;
            }

            _logger.LogInformation("Starting mapper from bankAccount to bankAccountView.");

            var bankAccountView = _mapper.Map<BankAccountView>(bankAccount);

            _logger.LogInformation($"returning bankAccountView {JsonSerializer.Serialize(bankAccountView)}");

            return bankAccountView;
        }
    }
}