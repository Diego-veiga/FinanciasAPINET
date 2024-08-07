using financiasapi.src.dtos;
using MediatR;

namespace financias.src.query.BankAccount
{
    public class GetBankAccountAllByUserId:IRequest<List<BankAccountView>>
    {
        public Guid UserId { get; set; }
    }
}