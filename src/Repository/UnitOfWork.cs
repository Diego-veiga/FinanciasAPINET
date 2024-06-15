using financias.src.database.Context;
using financias.src.interfaces;

namespace financias.src.Repository
{
    public class UnitOfWork : IUnitOFWork
    {
        private UserRepository _userRepository;
        private BanckRepository _banckRepository;
        private BanckAccountRepository _banckAccountRepository;
        private UserBanckAccountRepository _userBanckAccountRepository;
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
        public IBanckAccountRepository banckAccountRepository
        {
            get
            {
                return _banckAccountRepository = _banckAccountRepository ?? new BanckAccountRepository(_context);

            }
        }
        public IUserBancksAccountsRepository userBancksAccountsRepository
        {
            get
            {
                return _userBanckAccountRepository = _userBanckAccountRepository ?? new UserBanckAccountRepository(_context);

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