using financiasapi.src.dtos;
using MediatR;

namespace financiasapi.src.query.Banks
{
    public class GetBankById:IRequest<BankView>
    {
        public Guid Id { get; set; }
    }
}