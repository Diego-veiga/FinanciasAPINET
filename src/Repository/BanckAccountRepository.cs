using financias.src.database.Context;
using financias.src.interfaces;
using financias.src.models;
using financias.src.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace financias.src.Repository
{
    public class BanckAccountRepository: BaseRepository<BanckAccount>, IBanckAccountRepository
    {
        public BanckAccountRepository(AppDbContext context):base(context){
            
        }

        public async Task<List<BanckAccount>> GetByUserId(Guid userId)
        {
           
                var banckAccountList = await (from ba in _context.BanckAccounts
                join uba in _context.UserBancksAccounts 
                on ba.Id equals uba.BanckAccountId 
                where uba.UserId == userId
                select ba).ToListAsync();
                return banckAccountList ;                
        }
    }
}