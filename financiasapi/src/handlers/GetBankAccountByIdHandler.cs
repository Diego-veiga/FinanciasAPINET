using System.Text.Json;
using AutoMapper;
using financias.src.interfaces;
using financias.src.query.BanckAccount;
using financiasapi.src.dtos;
using MediatR;

namespace financias.src.handlers
{
    public class GetBanckAccountByIdHandler : IRequestHandler<GetBankAccountById, BankAccountView>
    {
        private IUnitOFWork _unitOFWork;
        private readonly IMapper _mapper;
        public ILogger<GetBankAccountAllByUserIdHandler> _logger { get; set; }

        public GetBanckAccountByIdHandler(IUnitOFWork unitOFWork, IMapper mapper,ILogger<GetBankAccountAllByUserIdHandler> logger)
        {
            _unitOFWork = unitOFWork;
            _mapper = mapper;
            _logger=logger;

        }

        public async Task<BankAccountView> Handle(GetBankAccountById request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start GetBanckAccountByIdHandler with request {JsonSerializer.Serialize(request)}");

            var banckAccount = await _unitOFWork.banckAccountRepository.GetById(request.Id);

            _logger.LogInformation($"Return result banckAccountRepository.GetById {JsonSerializer.Serialize(banckAccount)}");

            if (banckAccount is null)
            {
                 _logger.LogInformation($"Returning banckAccount null");

                return null;
            }

            _logger.LogInformation("Starting mappper from banckAccount to banckAccoutnView.");

            var banckAccoutnView = _mapper.Map<BankAccountView>(banckAccount);

            _logger.LogInformation($"returning banckAccoutnView {JsonSerializer.Serialize(banckAccoutnView)}");

            return banckAccoutnView;
        }
    }
}