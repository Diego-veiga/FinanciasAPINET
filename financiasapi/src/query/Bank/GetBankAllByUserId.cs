using financiasapi.src.dtos;
using MediatR;

namespace financiasapi.src.query.Bank
{
    public class GetBankAllByUserId : IRequest<List<BankView>>
    {
        public Guid UserId { get; set; }
    }
}
