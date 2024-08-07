using financias.src.database.Context;
using financias.src.interfaces;

namespace financias.src.Repository
{
    public class UnitOfWork : IUnitOFWork
    {
        private UserRepository _userRepository;
        private BankRepository _bankRepository;
        private BankAccountRepository _bankAccountRepository;
        private UserBankAccountRepository _userBankAccountRepository;
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

        public IBankRepository bankRepository
        {
            get
            {
                return _bankRepository = _bankRepository ?? new BankRepository(_context);

            }
        }
        public IBankAccountRepository bankAccountRepository
        {
            get
            {
                return _bankAccountRepository = _bankAccountRepository ?? new BankAccountRepository(_context);

            }
        }

        public IUserBanksAccountsRepository userBanksAccountsRepository
        {
            get
            {
                return _userBankAccountRepository = _userBankAccountRepository ?? new UserBankAccountRepository(_context);

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