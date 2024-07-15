using System.Text.Json;
using AutoMapper;
using financias.src.interfaces;
using financias.src.query.BanckAccount;
using financiasapi.src.dtos;
using MediatR;

namespace financias.src.handlers
{
    public class GetBanckAccountAllByUserIdHandler : IRequestHandler<GetBanckAccountAllByUserId, List<BanckAccountView>>
    {
        private IUnitOFWork _unitOFWork;
        private readonly IMapper _mapper;
        public ILogger<GetBanckAccountAllByUserIdHandler> _logger { get; set; }

        public GetBanckAccountAllByUserIdHandler(IUnitOFWork unitOFWork, IMapper mapper,ILogger<GetBanckAccountAllByUserIdHandler> logger)
        {
            _unitOFWork = unitOFWork;
            _mapper = mapper;
            _logger =logger;
        }

        public async Task<List<BanckAccountView>> Handle(GetBanckAccountAllByUserId request, CancellationToken cancellationToken)
        {
              _logger.LogInformation($"Start GetBanckAccountAllByUserIdHandler with request {JsonSerializer.Serialize(request)}");

            var banckAccountViews = new List<BanckAccountView>();
            var banckAccounts = await _unitOFWork.banckAccountRepository.GetByUserId(request.UserId);

             _logger.LogInformation($"Return result banckAccountRepository.GetByUserId {JsonSerializer.Serialize(banckAccounts)}");
                                                                       
                                                                       
            if (banckAccounts.Count < 0)
            {
                _logger.LogInformation($"returning banckAccountViews {JsonSerializer.Serialize(banckAccountViews)}");

                return banckAccountViews;
            }

            _logger.LogInformation("Starting mappper from banckAccounts to banckAccountViews.");
           
             foreach(var banckAccount in  banckAccounts ){

                    banckAccountViews.Add(_mapper.Map<BanckAccountView>(banckAccount)) ;
             }
             _logger.LogInformation($"returning banckAccountViews {JsonSerializer.Serialize(banckAccountViews)}");

            return banckAccountViews;
        }
    }
}