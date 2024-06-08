using financias.src.database.Context;
using financias.src.interfaces;

namespace financias.src.Repository
{
    public class UnitOfWork : IUnitOFWork
    {
        private UserRepository _userRepository;
        private BanckRepository _banckRepository;
        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IUserRepository userRepository
        {
            get
            {
                return _userRepository = _userRepository ?? new UserRepository(_context);

            }
        }

        public IBanckRepository banckRepository
        {
            get
            {
                return _banckRepository = _banckRepository ?? new BanckRepository(_context);

            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}