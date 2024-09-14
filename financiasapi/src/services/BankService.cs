using AutoMapper;
using financias.src.interfaces;
using financiasapi.src.dtos;
using financiasapi.src.models;
using Microsoft.EntityFrameworkCore;

namespace financias.src.services
{
    public class BankService : IBankService
    {
        private IUnitOFWork _unitOFWork;
        private readonly IMapper _mapper;
        public BankService(IUnitOFWork unitOFWork, IMapper mapper)
        {
            _unitOFWork = unitOFWork;
            _mapper = mapper;

        }
        
        public async Task Delete(Guid id)
        {
            var banck = await _unitOFWork.bankRepository.GetById(id);
            if (banck is null)
            {
                throw new ApplicationException("banck not found ");
            }
            if (banck.UserId == null)
            {
                throw new ApplicationException("default banck can't deleted ");

            }

            banck.Active = false;
            _unitOFWork.bankRepository.Delete(banck!);
            await _unitOFWork.Commit();

        }

        public async Task<List<BankView>> GetActive(Guid userId)
        {
            var bancks = await _unitOFWork.bankRepository
            .Get()
            .Where(b => b.UserId == userId || b.UserId == null)
            .ToListAsync();
            return _mapper.Map<List<BankView>>(bancks);
        }

        public async Task<BankView> GetById(Guid id)
        {
            var banck = await _unitOFWork.bankRepository.GetById(id);
            if (banck is null)
            {
                throw new ApplicationException("banck not found ");
            }
            return _mapper.Map<BankView>(banck);

        }

        public async Task<List<BankView>> GetByUserId(Guid userId)
        {
            var banck = await _unitOFWork.bankRepository.GetByUserId(userId);
            return _mapper.Map<List<BankView>>(banck);
        }

        public async Task Update(UpdateBank updateBanck)
        {
            var banck = await _unitOFWork.bankRepository.GetById(updateBanck.Id);

            if (banck is null)
            {
                throw new ApplicationException("banck not found ");
            }
            banck.Cnpj = updateBanck.Cnpj.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
            banck.Name = updateBanck.Name;
            banck.UpdatedAt = DateTime.Now;
            _unitOFWork.bankRepository.Update(banck!);
            await _unitOFWork.Commit();
        }
    }
}