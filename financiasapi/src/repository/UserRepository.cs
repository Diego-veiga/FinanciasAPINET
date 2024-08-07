using financias.src.database.Context;
using financias.src.interfaces;
using financias.src.Repository.Base;
using financiasapi.src.models;
using Microsoft.EntityFrameworkCore;

namespace financias.src.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}