
using financiasapi.src.models;

namespace financias.src.interfaces
{
    public interface IBankAccountRepository: IRepository<BankAccount>
    {
        Task<List<BankAccount>> GetByUserId(Guid userId);
        
    }
}