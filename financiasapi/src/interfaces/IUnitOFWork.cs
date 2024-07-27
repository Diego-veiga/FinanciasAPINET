
namespace financias.src.interfaces
{
    public interface IUnitOFWork
    {
        IUserRepository userRepository { get; }
        IBankRepository bankRepository { get; }
        IBankAccountRepository bankAccountRepository { get; }
        IUserBancksAccountsRepository userBancksAccountsRepository { get; }
        Task Commit();
    }
}