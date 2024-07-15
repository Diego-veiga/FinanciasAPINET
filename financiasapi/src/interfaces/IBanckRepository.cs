
using financiasapi.src.models;

namespace financias.src.interfaces
{
    public interface IBanckRepository : IRepository<Banck>
    {
        Task<List<Banck>> GetByUserId(Guid userId);

    }
}