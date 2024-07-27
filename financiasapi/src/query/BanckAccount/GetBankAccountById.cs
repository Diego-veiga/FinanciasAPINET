using financiasapi.src.dtos;
using MediatR;

namespace financias.src.query.BankAccount
{
    public class GetBankAccountById:IRequest<BankAccountView>
    {
        public Guid Id { get; set; }
    }
}