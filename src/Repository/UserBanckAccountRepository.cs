using financias.src.database.Context;
using financias.src.interfaces;
using financias.src.models;
using financias.src.Repository.Base;

namespace financias.src.Repository
{
    public class UserBanckAccountRepository: BaseRepository<UserBancksAccounts>, IUserBancksAccountsRepository
    {
        public UserBanckAccountRepository(AppDbContext context):base(context){

        }
        
        
    }
}