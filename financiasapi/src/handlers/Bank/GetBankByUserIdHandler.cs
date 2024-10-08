using AutoMapper;
using financias.src.handlers;
using financias.src.interfaces;
using financiasapi.src.dtos;
using financiasapi.src.models;
using financiasapi.src.query.Bank;
using MediatR;

namespace financiasapi.src.handlers.Bank
{
    public class GetBankByUserIdHandler : IRequestHandler<GetBankByUserId, List<BankView>>
    {
        private IUnitOFWork _unitOFWork;
        private readonly IMapper _mapper;
        public ILogger<GetBankAccountAllByUserIdHandler> _logger { get; set; }

        public GetBankByUserIdHandler(IUnitOFWork unitOFWork, IMapper mapper, ILogger<GetBankAccountAllByUserIdHandler> logger)
        {
            _unitOFWork = unitOFWork;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<List<BankView>> Handle(GetBankByUserId request, CancellationToken cancellationToken)
        {
            var banck = await _unitOFWork.bankRepository.GetByUserId(request.UserId);
            return _mapper.Map<List<BankView>>(banck);
        }
    }
}
