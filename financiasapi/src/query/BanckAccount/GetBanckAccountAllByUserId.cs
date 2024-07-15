using financiasapi.src.dtos;
using MediatR;

namespace financias.src.query.BanckAccount
{
    public class GetBanckAccountAllByUserId:IRequest<List<BanckAccountView>>
    {
        public Guid UserId { get; set; }
    }
}