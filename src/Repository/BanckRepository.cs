using financias.src.database.Context;
using financias.src.interfaces;
using financias.src.models;
using financias.src.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace financias.src.Repository
{
    public class BanckRepository : BaseRepository<Banck>, IBanckRepository
    {
        public BanckRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<List<Banck>> GetByUserId(Guid userId)
        {
            return await _context.Bancks.Where(u => u.UserId == userId)
                                        .Include(u => u.User)
                                        .ToListAsync();
        }
    }
}