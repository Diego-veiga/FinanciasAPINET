using System.Text.Json;
using AutoMapper;
using financias.src.DTOs;
using financias.src.interfaces;
using financias.src.query.BanckAccount;
using MediatR;

namespace financias.src.handlers
{
    public class GetBanckAccountByIdHandler : IRequestHandler<GetBanckAccountById, BanckAccountView>
    {
        private IUnitOFWork _unitOFWork;
        private readonly IMapper _mapper;
        public ILogger<GetBanckAccountAllByUserIdHandler> _logger { get; set; }

        public GetBanckAccountByIdHandler(IUnitOFWork unitOFWork, IMapper mapper,ILogger<GetBanckAccountAllByUserIdHandler> logger)
        {
            _unitOFWork = unitOFWork;
            _mapper = mapper;
            _logger=logger;

        }

        public async Task<BanckAccountView> Handle(GetBanckAccountById request, CancellationToken cancellationToken)
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

            var banckAccoutnView = _mapper.Map<BanckAccountView>(banckAccount);

            _logger.LogInformation($"returning banckAccoutnView {JsonSerializer.Serialize(banckAccoutnView)}");

            return banckAccoutnView;
        }
    }
}