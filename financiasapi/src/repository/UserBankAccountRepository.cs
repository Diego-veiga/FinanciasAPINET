using financias.src.database.Context;
using financias.src.interfaces;
using financias.src.Repository.Base;
using financiasapi.src.models;

namespace financias.src.Repository
{
    public class UserBankAccountRepository: BaseRepository<UserBanksAccounts>, IUserBanksAccountsRepository
    {
        public UserBankAccountRepository(AppDbContext context):base(context){

        }

      
    }
}