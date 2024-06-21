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

        public GetBanckAccountByIdHandler(IUnitOFWork unitOFWork, IMapper mapper)
        {
            _unitOFWork = unitOFWork;
            _mapper = mapper;

        }

        public async Task<BanckAccountView> Handle(GetBanckAccountById request, CancellationToken cancellationToken)
        {
            var banckAccount = await _unitOFWork.banckAccountRepository.GetById(request.Id);
            if (banckAccount is null)
            {
                return null;
            }

            var banckAccoutnView = _mapper.Map<BanckAccountView>(banckAccount);

            return banckAccoutnView;
        }
    }
}