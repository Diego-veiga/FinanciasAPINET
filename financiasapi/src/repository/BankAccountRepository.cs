using financias.src.database.Context;
using financias.src.interfaces;
using financias.src.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace financias.src.Repository
{
    public class BankAccountRepository: BaseRepository<financiasapi.src.models.BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(AppDbContext context):base(context){
            
        }

        public async Task<List<financiasapi.src.models.BankAccount>> GetByUserId(Guid userId)
        {

                var bankAccountList = await (from ba in _context.BankAccounts
                join uba in _context.UserBanksAccounts 
                on ba.Id equals uba.BankAccountId 
                where uba.UserId == userId
                select ba).ToListAsync();
                return bankAccountList ;                
        }
    }
}