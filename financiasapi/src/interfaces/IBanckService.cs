using financiasapi.src.dtos;

namespace financias.src.interfaces
{
    public interface IBanckService
    {
        Task Create(CreateBanck createBanck);
        Task Delete(Guid id);
        Task Update(UpdateBanck updateBanck);
        Task<BanckView> GetById(Guid id);
        Task<List<BanckView>> GetActive(Guid userId);
        Task<List<BanckView>> GetByUserId(Guid userId);

    }
}