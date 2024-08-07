
using financiasapi.src.models;

namespace financias.src.interfaces
{
    public interface IBankRepository : IRepository<Bank>
    {
        Task<List<Bank>> GetByUserId(Guid userId);

    }
}