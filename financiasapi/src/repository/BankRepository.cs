using financias.src.database.Context;
using financias.src.interfaces;
using financias.src.Repository.Base;
using financiasapi.src.models;
using Microsoft.EntityFrameworkCore;

namespace financias.src.Repository
{
    public class BankRepository : BaseRepository<Bank>, IBankRepository
    {
        public BankRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<List<Bank>> GetByUserId(Guid userId)
        {
            return await _context.Banks.Where(u => u.UserId == userId)
                                        .Include(u => u.User)
                                        .ToListAsync();
        }
    }
}