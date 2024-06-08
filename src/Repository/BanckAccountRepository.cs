using financias.src.database.Context;
using financias.src.interfaces;
using financias.src.models;
using financias.src.Repository.Base;

namespace financias.src.Repository
{
    public class BanckAccountRepository: BaseRepository<BanckAccount>, IBanckAccount
    {
        public BanckAccountRepository(AppDbContext context):base(context){
            
        }
        
    }
}