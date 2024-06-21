using financias.src.DTOs;
using MediatR;

namespace financias.src.query.BanckAccount
{
    public class GetBanckAccountById:IRequest<BanckAccountView>
    {
        public Guid Id { get; set; }
    }
}