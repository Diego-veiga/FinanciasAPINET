using financias.src.models;

namespace financias.src.interfaces
{
    public interface IBanckAccountRepository: IRepository<BanckAccount>
    {
        Task<List<BanckAccount>> GetByUserId(Guid userId);
        
    }
}