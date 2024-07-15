using AutoMapper;
using financias.src.interfaces;
using financiasapi.src.dtos;
using financiasapi.src.models;
using Microsoft.EntityFrameworkCore;

namespace financias.src.services
{
    public class BanckService : IBanckService
    {
        private IUnitOFWork _unitOFWork;
        private readonly IMapper _mapper;
        public BanckService(IUnitOFWork unitOFWork, IMapper mapper)
        {
            _unitOFWork = unitOFWork;
            _mapper = mapper;

        }
        public async Task Create(CreateBanck createBanck)
        {
            var user = _mapper.Map<Banck>(createBanck);
            _unitOFWork.banckRepository.Add(user!);
            await _unitOFWork.Commit();
        }

        public async Task Delete(Guid id)
        {
            var banck = await _unitOFWork.banckRepository.GetById(id);
            if (banck is null)
            {
                throw new ApplicationException("banck not found ");
            }
            if (banck.UserId == null)
            {
                throw new ApplicationException("default banck can't deleted ");

            }

            banck.Active = false;
            _unitOFWork.banckRepository.Delete(banck!);
            await _unitOFWork.Commit();

        }

        public async Task<List<BanckView>> GetActive(Guid userId)
        {
            var bancks = await _unitOFWork.banckRepository
            .Get()
            .Where(b => b.UserId == userId || b.UserId == null)
            .ToListAsync();
            return _mapper.Map<List<BanckView>>(bancks);
        }

        public async Task<BanckView> GetById(Guid id)
        {
            var banck = await _unitOFWork.banckRepository.GetById(id);
            if (banck is null)
            {
                throw new ApplicationException("banck not found ");
            }
            return _mapper.Map<BanckView>(banck);

        }

        public async Task<List<BanckView>> GetByUserId(Guid userId)
        {
            var banck = await _unitOFWork.banckRepository.GetByUserId(userId);
            return _mapper.Map<List<BanckView>>(banck);
        }

        public async Task Update(UpdateBanck updateBanck)
        {
            var banck = await _unitOFWork.banckRepository.GetById(updateBanck.Id);

            if (banck is null)
            {
                throw new ApplicationException("banck not found ");
            }
            banck.Cnpj = updateBanck.Cnpj.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
            banck.Name = updateBanck.Name;
            banck.UpdatedAt = DateTime.Now;
            _unitOFWork.banckRepository.Update(banck!);
            await _unitOFWork.Commit();
        }
    }
}