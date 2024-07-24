
namespace financias.src.interfaces
{
    public interface IUnitOFWork
    {
        IUserRepository userRepository { get; }
        IBankRepository bankRepository { get; }
        IBanckAccountRepository banckAccountRepository { get; }
        IUserBancksAccountsRepository userBancksAccountsRepository { get; }
        Task Commit();
    }
}