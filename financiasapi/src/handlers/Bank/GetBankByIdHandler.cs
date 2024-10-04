using System.Text.Json;
using AutoMapper;
using financias.src.interfaces;
using financiasapi.src.dtos;
using financiasapi.src.query.Banks;
using MediatR;

namespace financiasapi.src.handlers.Bank
{
    public class GetBankByIdHandler : IRequestHandler<GetBankById, BankView>
    {
        private  IUnitOFWork _unitOFWork;
        private readonly IMapper _mapper;
        public ILogger<GetBankByIdHandler> _logger;

        public GetBankByIdHandler(IUnitOFWork unitOFWork, IMapper mapper,ILogger<GetBankByIdHandler> logger)
        {
            _unitOFWork = unitOFWork;
            _mapper = mapper; 
            _logger = logger;
        }
        public async Task<BankView> Handle(GetBankById request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start GetBankByIdHandler with request {JsonSerializer.Serialize(request)}");
             var bank = await _unitOFWork.bankRepository.GetById(request.Id);
            _logger.LogInformation($"Return result bankRepository.GetById {JsonSerializer.Serialize(bank)}");

            if (bank is null)
             {
                 _logger.LogInformation($"Returning bank null");
                 return null;
             }

            _logger.LogInformation("Starting mapper from bank to bankView.");

            var bankView = _mapper.Map<BankView>(bank);

            _logger.LogInformation($"returning bankAccountView {JsonSerializer.Serialize(bankView)}");

            return bankView;
        }
    }
}