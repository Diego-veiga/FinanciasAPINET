using System.Text.Json;
using AutoMapper;
using financias.src.interfaces;
using financias.src.query.BankAccount;
using financiasapi.src.dtos;
using MediatR;

namespace financias.src.handlers
{
    public class GetBankAccountAllByUserIdHandler : IRequestHandler<GetBankAccountAllByUserId, List<BankAccountView>>
    {
        private IUnitOFWork _unitOFWork;
        private readonly IMapper _mapper;
        public ILogger<GetBankAccountAllByUserIdHandler> _logger { get; set; }

        public GetBankAccountAllByUserIdHandler(IUnitOFWork unitOFWork, IMapper mapper,ILogger<GetBankAccountAllByUserIdHandler> logger)
        {
            _unitOFWork = unitOFWork;
            _mapper = mapper;
            _logger =logger;
        }

        public async Task<List<BankAccountView>> Handle(GetBankAccountAllByUserId request, CancellationToken cancellationToken)
        {
              _logger.LogInformation($"Start GetBankAccountAllByUserIdHandler with request {JsonSerializer.Serialize(request)}");

            var bankAccountViews = new List<BankAccountView>();
            var bankAccounts = await _unitOFWork.bankAccountRepository.GetByUserId(request.UserId);

             _logger.LogInformation($"Return result bankAccountRepository.GetByUserId {JsonSerializer.Serialize(bankAccounts)}");
                                                                       
                                                                       
            if (bankAccounts.Count < 0)
            {
                _logger.LogInformation($"returning bankAccountViews {JsonSerializer.Serialize(bankAccountViews)}");

                return bankAccountViews;
            }

            _logger.LogInformation("Starting mapper from bankAccounts to bankAccountViews.");
           
             foreach(var bankAccount in  bankAccounts ){

                    bankAccountViews.Add(_mapper.Map<BankAccountView>(bankAccount)) ;
             }
             _logger.LogInformation($"returning bankAccountViews {JsonSerializer.Serialize(bankAccountViews)}");

            return bankAccountViews;
        }
    }
}