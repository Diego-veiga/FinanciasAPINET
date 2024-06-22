using AutoMapper;
using financias.src.DTOs;
using financias.src.interfaces;
using financias.src.query.BanckAccount;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace financias.src.handlers
{
    public class GetBanckAccountAllByUserIdHandler : IRequestHandler<GetBanckAccountAllByUserId, List<BanckAccountView>>
    {
        private IUnitOFWork _unitOFWork;
        private readonly IMapper _mapper;

        public GetBanckAccountAllByUserIdHandler(IUnitOFWork unitOFWork, IMapper mapper)
        {
            _unitOFWork = unitOFWork;
            _mapper = mapper;

        }

        public async Task<List<BanckAccountView>> Handle(GetBanckAccountAllByUserId request, CancellationToken cancellationToken)
        {
            var banckAccountViews = new List<BanckAccountView>();
            var banckAccounts = await _unitOFWork.banckAccountRepository.GetByUserId(request.UserId);
                                                                       
                                                                       
            if (banckAccounts.Count < 0)
            {
                return banckAccountViews;
            }
             foreach(var banckAccount in  banckAccounts ){

                    banckAccountViews.Add(_mapper.Map<BanckAccountView>(banckAccount)) ;
             }

            return banckAccountViews;
        }
    }
}