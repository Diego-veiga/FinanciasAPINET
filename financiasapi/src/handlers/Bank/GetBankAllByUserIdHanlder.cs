using AutoMapper;
using financias.src.handlers;
using financias.src.interfaces;
using financiasapi.src.dtos;
using financiasapi.src.models;
using financiasapi.src.query.Bank;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace financiasapi.src.handlers.Bank
{
    public class GetBankAllByUserIdHanlder : IRequestHandler<GetBankAllByUserId, List<BankView>>
    {
        private IUnitOFWork _unitOFWork;
        private readonly IMapper _mapper;
        public ILogger<GetBankAccountAllByUserIdHandler> _logger { get; set; }

        public GetBankAllByUserIdHanlder(IUnitOFWork unitOFWork, IMapper mapper, ILogger<GetBankAccountAllByUserIdHandler> logger)
        {
            _unitOFWork = unitOFWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<BankView>> Handle(GetBankAllByUserId request, CancellationToken cancellationToken)
        {
            var banks = await _unitOFWork.bankRepository.Get().Where(b => b.UserId == request.UserId || b.UserId == null).ToListAsync();
            return _mapper.Map<List<BankView>>(banks);
        }
    }
}
