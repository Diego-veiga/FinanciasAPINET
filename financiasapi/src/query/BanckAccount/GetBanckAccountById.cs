using financiasapi.src.dtos;
using MediatR;

namespace financias.src.query.BanckAccount
{
    public class GetBanckAccountById:IRequest<BanckAccountView>
    {
        public Guid Id { get; set; }
    }
}