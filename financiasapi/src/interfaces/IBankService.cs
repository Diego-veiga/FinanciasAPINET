using financiasapi.src.dtos;

namespace financias.src.interfaces
{
    public interface IBankService
    {
        //Task Create(CreateBank createBank);
        Task Delete(Guid id);
        Task Update(UpdateBank updateBank);
        Task<BankView> GetById(Guid id);
        Task<List<BankView>> GetActive(Guid userId);
        Task<List<BankView>> GetByUserId(Guid userId);

    }
}